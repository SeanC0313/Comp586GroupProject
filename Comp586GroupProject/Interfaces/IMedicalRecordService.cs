using Comp586GroupProject.Models;

namespace Comp586GroupProject.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(int patientId);
        Task<MedicalRecord?> GetByIdAsync(int medicalRecordId);
        Task<MedicalRecord> AddAsync(MedicalRecord record);
        Task<MedicalRecord?> UpdateAsync(MedicalRecord record);
        Task<bool> DeleteAsync(int medicalRecordId);
    }
}
