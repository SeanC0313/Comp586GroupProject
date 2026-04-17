using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class StaffService : EfCoreServiceBase, IStaffService
    {
        public StaffService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Staff>> GetAllAsync(CancellationToken cancellationToken = default) =>
            WithDbAsync(async db => (await db.Staffs
                .AsNoTracking()
                .Include(s => s.Role)
                .ToListAsync(cancellationToken)).AsEnumerable());

        public Task<Staff?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
            WithDbAsync(db => db.Staffs
                .AsNoTracking()
                .Include(s => s.Role)
                .FirstOrDefaultAsync(s => s.Email == email, cancellationToken));

        public Task<Staff?> GetByIdAsync(int staffId, CancellationToken cancellationToken = default) =>
            WithDbAsync(db => db.Staffs
                .AsNoTracking()
                .Include(s => s.Role)
                .FirstOrDefaultAsync(s => s.StaffID == staffId, cancellationToken));

        public Task<Staff> AddStaffAsync(Staff staff, string plainPassword, CancellationToken cancellationToken = default) =>
            WithDbAsync(async db =>
            {
                staff.PassWordHash = StaffPasswordHasher.Hash(staff, plainPassword);
                db.Staffs.Add(staff);
                await db.SaveChangesAsync(cancellationToken);
                return staff;
            });

        public Task<Staff?> UpdateStaffAsync(Staff staff, CancellationToken cancellationToken = default) =>
            WithDbAsync(async db =>
            {
                var existing = await db.Staffs.FindAsync([staff.StaffID], cancellationToken);
                if (existing is null)
                    return null;

                existing.FirstName = staff.FirstName;
                existing.LastName = staff.LastName;
                existing.Email = staff.Email;
                existing.Specialty = staff.Specialty;
                existing.RoleID = staff.RoleID;

                await db.SaveChangesAsync(cancellationToken);
                return existing;
            });

        public Task<bool> DeleteStaffAsync(int staffId, CancellationToken cancellationToken = default) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Staffs.FindAsync([staffId], cancellationToken);
                if (entity is null)
                    return false;

                db.Staffs.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
                return true;
            });
    }
}
