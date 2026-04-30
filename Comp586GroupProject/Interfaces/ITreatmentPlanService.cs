using Comp586GroupProject.Models;

namespace Comp586GroupProject.Interfaces
{
    public interface ITreatmentPlanService
    {
        Task<IEnumerable<TreatmentPlan>> GetByPatientIdAsync(int patientId);
        Task<TreatmentPlan?> GetByIdAsync(int treatmentPlanId);
        Task<TreatmentPlan> AddAsync(TreatmentPlan plan);
        Task<TreatmentPlan?> UpdateAsync(TreatmentPlan plan);
        Task<bool> DeleteAsync(int treatmentPlanId);
    }
}
