using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class TreatmentPlanService : EfCoreServiceBase, ITreatmentPlanService
    {
        public TreatmentPlanService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<TreatmentPlan>> GetByPatientIdAsync(int patientId) =>
            WithDbAsync(async db => (await db.TreatmentPlans
                .AsNoTracking()
                .Include(t => t.Staff)
                .Where(t => t.PatientId == patientId)
                .OrderByDescending(t => t.StartDate)
                .ToListAsync()).AsEnumerable());

        public Task<TreatmentPlan?> GetByIdAsync(int treatmentPlanId) =>
            WithDbAsync(db => db.TreatmentPlans
                .AsNoTracking()
                .Include(t => t.Patient)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(t => t.TreatmentPlanId == treatmentPlanId));

        public Task<TreatmentPlan> AddAsync(TreatmentPlan plan) =>
            WithDbAsync(async db =>
            {
                db.TreatmentPlans.Add(plan);
                await db.SaveChangesAsync();
                return plan;
            });

        public Task<TreatmentPlan?> UpdateAsync(TreatmentPlan plan) =>
            WithDbAsync(async db =>
            {
                var existing = await db.TreatmentPlans.FindAsync(plan.TreatmentPlanId);
                if (existing is null)
                    return null;

                existing.Title = plan.Title;
                existing.Description = plan.Description;
                existing.StartDate = plan.StartDate;
                existing.EndDate = plan.EndDate;
                existing.Status = plan.Status;
                existing.Notes = plan.Notes;
                existing.StaffId = plan.StaffId;

                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteAsync(int treatmentPlanId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.TreatmentPlans.FindAsync(treatmentPlanId);
                if (entity is null)
                    return false;

                db.TreatmentPlans.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
