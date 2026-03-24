using Comp586GroupProject.Data;
using Comp586GroupProject.Models;
using Comp586GroupProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Services
{
    public class BillingService : IBillingService
    {
        private readonly DatabaseContext _context;

        public BillingService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Billing>> GetAllBillingsAsync()
        {
            return await _context.Billings
                                 .Include(b => b.Patient)
                                 .Include(b => b.Appointment)
                                 .ToListAsync();
        }

        public async Task<Billing> GetBillingByIdAsync(int billingId)
        {
            return await _context.Billings
                                 .Include(b => b.Patient)
                                 .Include(b => b.Appointment)
                                 .FirstOrDefaultAsync(b => b.BillingId == billingId);
        }

        public async Task<Billing> CreateBillingAsync(Billing billing)
        {
            _context.Billings.Add(billing);
            await _context.SaveChangesAsync();
            return billing;
        }

        public async Task<Billing> UpdateBillingAsync(Billing billing)
        {
            var existing = await _context.Billings.FindAsync(billing.BillingId);
            if (existing == null) return null;

            existing.PatientId = billing.PatientId;
            existing.AppointmentId = billing.AppointmentId;
            existing.Amount = billing.Amount;
            existing.InsuranceCovered = billing.InsuranceCovered;
            existing.PaymentStatus = billing.PaymentStatus;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBillingAsync(int billingId)
        {
            var billing = await _context.Billings.FindAsync(billingId);
            if (billing == null) return false;

            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}