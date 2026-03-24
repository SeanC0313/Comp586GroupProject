using Comp586GroupProject.Data;
using Comp586GroupProject.Models;
using Comp586GroupProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly DatabaseContext _context;

        public MedicationService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationsAsync()
        {
            return await _context.Medications.ToListAsync();
        }

        public async Task<Medication> GetMedicationByIdAsync(int medicationId)
        {
            return await _context.Medications.FindAsync(medicationId);
        }

        public async Task<Medication> CreateMedicationAsync(Medication medication)
        {
            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();
            return medication;
        }

        public async Task<Medication> UpdateMedicationAsync(Medication medication)
        {
            var existingMedication = await _context.Medications.FindAsync(medication.MedicationId);
            if (existingMedication == null) return null;

            existingMedication.Name = medication.Name;
            existingMedication.Description = medication.Description;
            existingMedication.Stock = medication.Stock;

            _context.Medications.Update(existingMedication);
            await _context.SaveChangesAsync();
            return existingMedication;
        }

        public async Task<bool> DeleteMedicationAsync(int medicationId)
        {
            var medication = await _context.Medications.FindAsync(medicationId);
            if (medication == null) return false;

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}