using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class AppointmentService : IAppointmentInterface
    {
        private readonly DatabaseContext _context;

        public AppointmentService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                                 .Include(a => a.Patient)
                                 .Include(a => a.Staff)
                                 .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                                 .Include(a => a.Patient)
                                 .Include(a => a.Staff)
                                 .FirstOrDefaultAsync(a => a.AppointmentID == id);
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}