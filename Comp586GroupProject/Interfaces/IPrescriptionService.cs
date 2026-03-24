using Comp586GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Interfaces
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> GetPrescriptionByIdAsync(int prescriptionId);
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId);
        Task<Prescription> CreatePrescriptionAsync(Prescription prescription);
        Task<Prescription> UpdatePrescriptionAsync(Prescription prescription);
        Task<bool> DeletePrescriptionAsync(int prescriptionId);
    }
}