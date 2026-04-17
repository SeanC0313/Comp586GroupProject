using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class InsuranceService : EfCoreServiceBase, IInsuranceService
    {
        public InsuranceService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Insurance>> GetAllInsurancesAsync() =>
            WithDbAsync(async db => (await db.Insurances.ToListAsync()).AsEnumerable());

        public Task<Insurance?> GetInsuranceByIdAsync(int insuranceId) =>
            WithDbAsync(async db => await db.Insurances.FindAsync(insuranceId));

        public Task<Insurance> CreateInsuranceAsync(Insurance insurance) =>
            WithDbAsync(async db =>
            {
                db.Insurances.Add(insurance);
                await db.SaveChangesAsync();
                return insurance;
            });

        public Task<Insurance?> UpdateInsuranceAsync(Insurance insurance) =>
            WithDbAsync(async db =>
            {
                var existing = await db.Insurances.FindAsync(insurance.InsuranceId);
                if (existing is null)
                    return null;

                existing.ProviderName = insurance.ProviderName;
                existing.Phone = insurance.Phone;
                existing.CoverageDetails = insurance.CoverageDetails;

                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteInsuranceAsync(int insuranceId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Insurances.FindAsync(insuranceId);
                if (entity is null)
                    return false;

                db.Insurances.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
