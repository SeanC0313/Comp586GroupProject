using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class MedicalRecordService : EfCoreServiceBase, IMedicalRecordService
    {
        public MedicalRecordService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(int patientId) =>
            WithDbAsync(async db => (await db.MedicalRecords
                .AsNoTracking()
                .Include(m => m.Staff)
                .Where(m => m.PatientId == patientId)
                .OrderByDescending(m => m.RecordedAt)
                .ToListAsync()).AsEnumerable());

        public Task<MedicalRecord?> GetByIdAsync(int medicalRecordId) =>
            WithDbAsync(db => db.MedicalRecords
                .AsNoTracking()
                .Include(m => m.Patient)
                .Include(m => m.Staff)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == medicalRecordId));

        public Task<MedicalRecord> AddAsync(MedicalRecord record) =>
            WithDbAsync(async db =>
            {
                db.MedicalRecords.Add(record);
                await db.SaveChangesAsync();
                return record;
            });

        public Task<MedicalRecord?> UpdateAsync(MedicalRecord record) =>
            WithDbAsync(async db =>
            {
                var existing = await db.MedicalRecords.FindAsync(record.MedicalRecordId);
                if (existing is null)
                    return null;

                existing.Diagnosis = record.Diagnosis;
                existing.Symptoms = record.Symptoms;
                existing.Notes = record.Notes;
                existing.StaffId = record.StaffId;

                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteAsync(int medicalRecordId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.MedicalRecords.FindAsync(medicalRecordId);
                if (entity is null)
                    return false;

                db.MedicalRecords.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
