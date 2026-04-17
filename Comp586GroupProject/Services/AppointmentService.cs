using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class AppointmentService : EfCoreServiceBase, IAppointmentInterface
    {
        public AppointmentService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync() =>
            WithDbAsync(async db => (await db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Staff)
                .ToListAsync()).AsEnumerable());

        public Task<Appointment?> GetAppointmentByIdAsync(int id) =>
            WithDbAsync(db => db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(a => a.AppointmentID == id));

        public Task<Appointment> AddAppointmentAsync(Appointment appointment) =>
            WithDbAsync(async db =>
            {
                db.Appointments.Add(appointment);
                await db.SaveChangesAsync();
                return appointment;
            });

        public Task<Appointment> UpdateAppointmentAsync(Appointment appointment) =>
            WithDbAsync(async db =>
            {
                db.Appointments.Update(appointment);
                await db.SaveChangesAsync();
                return appointment;
            });

        public Task<bool> DeleteAppointmentAsync(int id) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Appointments.FindAsync(id);
                if (entity is null)
                    return false;

                db.Appointments.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
