using Comp586GroupProject.Models;
using Comp586GroupProject.Data;
using Microsoft.EntityFrameworkCore;
using Comp586GroupProject.Interfaces;

namespace Comp586GroupProject.Services
{
    public class PrescriptionService : EfCoreServiceBase, IPrescriptionService
    {
        public PrescriptionService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync() =>
            WithDbAsync(async db => (await db.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Staff)
                .ToListAsync()).AsEnumerable());

        public Task<Prescription?> GetPrescriptionByIdAsync(int prescriptionId) =>
            WithDbAsync(db => db.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Staff)
                .FirstOrDefaultAsync(p => p.PrescriptionID == prescriptionId));

        public Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId) =>
            WithDbAsync(async db => (await db.Prescriptions
                .Include(p => p.Staff)
                .Where(p => p.PatientID == patientId)
                .ToListAsync()).AsEnumerable());

        public Task<Prescription> CreatePrescriptionAsync(Prescription prescription) =>
            WithDbAsync(async db =>
            {
                db.Prescriptions.Add(prescription);
                await db.SaveChangesAsync();
                return prescription;
            });

        public Task<Prescription?> UpdatePrescriptionAsync(Prescription prescription) =>
            WithDbAsync(async db =>
            {
                var existingPrescription = await db.Prescriptions.FindAsync(prescription.PrescriptionID);
                if (existingPrescription is null)
                    return null;

                existingPrescription.MedicationID = prescription.MedicationID;
                existingPrescription.Dosage = prescription.Dosage;
                existingPrescription.StartDate = prescription.StartDate;
                existingPrescription.EndDate = prescription.EndDate;

                db.Prescriptions.Update(existingPrescription);
                await db.SaveChangesAsync();

                return existingPrescription;
            });

        public Task<bool> DeletePrescriptionAsync(int prescriptionId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Prescriptions.FindAsync(prescriptionId);
                if (entity is null)
                    return false;

                db.Prescriptions.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
