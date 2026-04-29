using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Models.Reports;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class FinancialReportService : EfCoreServiceBase, IFinancialReportService
    {
        public FinancialReportService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<BillingSummary> GetSummaryAsync(DateTime? from = null, DateTime? to = null) =>
            WithDbAsync(async db =>
            {
                var query = db.Billings.AsNoTracking().AsQueryable();

                if (from.HasValue)
                    query = query.Where(b => b.Appointment.AppointmentDate >= from.Value);
                if (to.HasValue)
                    query = query.Where(b => b.Appointment.AppointmentDate <= to.Value);

                var bills = await query.Include(b => b.Appointment).ToListAsync();

                return new BillingSummary
                {
                    TotalBillCount = bills.Count,
                    TotalBilled    = bills.Sum(b => b.Amount),
                    TotalPaid      = bills.Where(b => b.PaymentStatus == "Paid").Sum(b => b.Amount),
                    TotalOutstanding = bills.Where(b => b.PaymentStatus != "Paid").Sum(b => b.Amount),
                    PaidCount      = bills.Count(b => b.PaymentStatus == "Paid"),
                    UnpaidCount    = bills.Count(b => b.PaymentStatus == "Unpaid"),
                    PendingCount   = bills.Count(b => b.PaymentStatus == "Pending")
                };
            });

        public Task<IEnumerable<Billing>> GetByPaymentStatusAsync(string status) =>
            WithDbAsync(async db => (await db.Billings
                .AsNoTracking()
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .Where(b => b.PaymentStatus == status)
                .OrderByDescending(b => b.BillingId)
                .ToListAsync()).AsEnumerable());

        public Task<IEnumerable<Billing>> GetByPatientAsync(int patientId) =>
            WithDbAsync(async db => (await db.Billings
                .AsNoTracking()
                .Include(b => b.Appointment)
                .Where(b => b.PatientId == patientId)
                .OrderByDescending(b => b.BillingId)
                .ToListAsync()).AsEnumerable());

        public Task<IEnumerable<PatientBillingReport>> GetPatientBillingReportAsync() =>
            WithDbAsync(async db =>
            {
                var results = await db.Billings
                    .AsNoTracking()
                    .Include(b => b.Patient)
                    .GroupBy(b => new { b.PatientId, b.Patient.FirstName, b.Patient.LastName })
                    .Select(g => new PatientBillingReport
                    {
                        PatientId       = g.Key.PatientId ?? 0,
                        PatientName     = g.Key.FirstName + " " + g.Key.LastName,
                        TotalBilled     = g.Sum(b => b.Amount),
                        TotalPaid       = g.Where(b => b.PaymentStatus == "Paid").Sum(b => b.Amount),
                        TotalOutstanding = g.Where(b => b.PaymentStatus != "Paid").Sum(b => b.Amount),
                        BillCount       = g.Count()
                    })
                    .OrderByDescending(r => r.TotalBilled)
                    .ToListAsync();

                return results.AsEnumerable();
            });

        public Task<IEnumerable<Billing>> GetOutstandingBillsAsync() =>
            WithDbAsync(async db => (await db.Billings
                .AsNoTracking()
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .Where(b => b.PaymentStatus != "Paid")
                .OrderByDescending(b => b.Amount)
                .ToListAsync()).AsEnumerable());
    }
}
