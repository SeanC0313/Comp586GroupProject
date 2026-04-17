using Comp586GroupProject.Data;
using Comp586GroupProject.Models;
using Comp586GroupProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class MedicationService : EfCoreServiceBase, IMedicationService
    {
        public MedicationService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Medication>> GetAllMedicationsAsync() =>
            WithDbAsync(async db => (await db.Medications.ToListAsync()).AsEnumerable());

        public Task<Medication?> GetMedicationByIdAsync(int medicationId) =>
            WithDbAsync(async db => await db.Medications.FindAsync(medicationId));

        public Task<Medication> CreateMedicationAsync(Medication medication) =>
            WithDbAsync(async db =>
            {
                db.Medications.Add(medication);
                await db.SaveChangesAsync();
                return medication;
            });

        public Task<Medication?> UpdateMedicationAsync(Medication medication) =>
            WithDbAsync(async db =>
            {
                var existingMedication = await db.Medications.FindAsync(medication.MedicationId);
                if (existingMedication is null)
                    return null;

                existingMedication.Name = medication.Name;
                existingMedication.Description = medication.Description;
                existingMedication.Stock = medication.Stock;

                db.Medications.Update(existingMedication);
                await db.SaveChangesAsync();
                return existingMedication;
            });

        public Task<bool> DeleteMedicationAsync(int medicationId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Medications.FindAsync(medicationId);
                if (entity is null)
                    return false;

                db.Medications.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
