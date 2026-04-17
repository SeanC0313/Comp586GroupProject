using Comp586GroupProject.Data;
using Comp586GroupProject.Models;
using Comp586GroupProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class BillingService : EfCoreServiceBase, IBillingService
    {
        public BillingService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Billing>> GetAllBillingsAsync() =>
            WithDbAsync(async db => (await db.Billings
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .ToListAsync()).AsEnumerable());

        public Task<Billing?> GetBillingByIdAsync(int billingId) =>
            WithDbAsync(db => db.Billings
                .Include(b => b.Patient)
                .Include(b => b.Appointment)
                .FirstOrDefaultAsync(b => b.BillingId == billingId));

        public Task<Billing> CreateBillingAsync(Billing billing) =>
            WithDbAsync(async db =>
            {
                db.Billings.Add(billing);
                await db.SaveChangesAsync();
                return billing;
            });

        public Task<Billing?> UpdateBillingAsync(Billing billing) =>
            WithDbAsync(async db =>
            {
                var existing = await db.Billings.FindAsync(billing.BillingId);
                if (existing is null)
                    return null;

                existing.PatientId = billing.PatientId;
                existing.AppointmentId = billing.AppointmentId;
                existing.Amount = billing.Amount;
                existing.InsuranceCovered = billing.InsuranceCovered;
                existing.PaymentStatus = billing.PaymentStatus;

                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteBillingAsync(int billingId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Billings.FindAsync(billingId);
                if (entity is null)
                    return false;

                db.Billings.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
