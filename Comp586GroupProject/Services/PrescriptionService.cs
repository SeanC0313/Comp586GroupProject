using Comp586GroupProject.Models;
using Comp586GroupProject.Data;
using Microsoft.EntityFrameworkCore;
using Comp586GroupProject.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly DatabaseContext _context;

        public PrescriptionService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions
                                 .Include(p => p.Patient)
                                 .Include(p => p.Staff)
                                 .ToListAsync();
        }

        public async Task<Prescription> GetPrescriptionByIdAsync(int prescriptionId)
        {
            return await _context.Prescriptions
                                 .Include(p => p.Patient)
                                 .Include(p => p.Staff)
                                 .FirstOrDefaultAsync(p => p.PrescriptionID == prescriptionId);
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            return await _context.Prescriptions
                                 .Include(p => p.Staff)
                                 .Where(p => p.PatientID == patientId)
                                 .ToListAsync();
        }

        public async Task<Prescription> CreatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<Prescription> UpdatePrescriptionAsync(Prescription prescription)
        {
            var existingPrescription = await _context.Prescriptions.FindAsync(prescription.PrescriptionID);
            if (existingPrescription == null) return null;

            // Update fields
            existingPrescription.MedicationID = prescription.MedicationID;
            existingPrescription.Dosage = prescription.Dosage;
            existingPrescription.StartDate = prescription.StartDate;
            existingPrescription.EndDate = prescription.EndDate;

            _context.Prescriptions.Update(existingPrescription);
            await _context.SaveChangesAsync();

            return existingPrescription;
        }

        public async Task<bool> DeletePrescriptionAsync(int prescriptionId)
        {
            var prescription = await _context.Prescriptions.FindAsync(prescriptionId);
            if (prescription == null) return false;

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}