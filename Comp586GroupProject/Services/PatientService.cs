using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class PatientService : EfCoreServiceBase, IPatientInterface
    {
        public PatientService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Patient>> GetAllPatientsAsync() =>
            WithDbAsync(async db => (await db.Patients
                .Include(p => p.Appointments)
                .Include(p => p.Prescriptions)
                .ToListAsync()).AsEnumerable());

        public Task<Patient?> GetPatientByIdAsync(int id) =>
            WithDbAsync(db => db.Patients
                .Include(p => p.Appointments)
                .Include(p => p.Prescriptions)
                .FirstOrDefaultAsync(p => p.PatientId == id));

        public Task<Patient> AddPatientAsync(Patient patient) =>
            WithDbAsync(async db =>
            {
                db.Patients.Add(patient);
                await db.SaveChangesAsync();
                return patient;
            });

        public Task<Patient> UpdatePatientAsync(Patient patient) =>
            WithDbAsync(async db =>
            {
                db.Patients.Update(patient);
                await db.SaveChangesAsync();
                return patient;
            });

        public Task<bool> DeletePatientAsync(int id) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Patients.FindAsync(id);
                if (entity is null)
                    return false;

                db.Patients.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
