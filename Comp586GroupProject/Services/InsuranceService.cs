using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly DatabaseContext _context;

        public InsuranceService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Insurance>> GetAllInsurancesAsync()
        {
            return await _context.Insurances.ToListAsync();
        }

        public async Task<Insurance> GetInsuranceByIdAsync(int insuranceId)
        {
            return await _context.Insurances.FindAsync(insuranceId);
        }

        public async Task<Insurance> CreateInsuranceAsync(Insurance insurance)
        {
            _context.Insurances.Add(insurance);
            await _context.SaveChangesAsync();
            return insurance;
        }

        public async Task<Insurance> UpdateInsuranceAsync(Insurance insurance)
        {
            var existing = await _context.Insurances.FindAsync(insurance.InsuranceId);
            if (existing == null) return null;

            existing.ProviderName = insurance.ProviderName;
            existing.Phone = insurance.Phone;
            existing.CoverageDetails = insurance.CoverageDetails;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteInsuranceAsync(int insuranceId)
        {
            var insurance = await _context.Insurances.FindAsync(insuranceId);
            if (insurance == null) return false;

            _context.Insurances.Remove(insurance);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}