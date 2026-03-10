using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using CompGroup586GroupProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class PatientService : IPatientInterface
    {
        private readonly DatabaseContext _context;

        public PatientService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                                 .Include(p => p.Appointments)
                                 .Include(p => p.Prescriptions)
                                 .ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                                 .Include(p => p.Appointments)
                                 .Include(p => p.Prescriptions)
                                 .FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}