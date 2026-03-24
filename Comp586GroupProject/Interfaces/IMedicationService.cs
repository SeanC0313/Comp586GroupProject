using Comp586GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Interfaces
{
    public interface IMedicationService
    {
        Task<IEnumerable<Medication>> GetAllMedicationsAsync();
        Task<Medication> GetMedicationByIdAsync(int medicationId);
        Task<Medication> CreateMedicationAsync(Medication medication);
        Task<Medication> UpdateMedicationAsync(Medication medication);
        Task<bool> DeleteMedicationAsync(int medicationId);
    }
}