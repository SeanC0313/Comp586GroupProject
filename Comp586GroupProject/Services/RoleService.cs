using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class RoleService : EfCoreServiceBase, IRoleService
    {
        public RoleService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync() =>
            WithDbAsync(async db => (await db.Roles.ToListAsync()).AsEnumerable());

        public Task<Role?> GetRoleByIdAsync(int roleId) =>
            WithDbAsync(async db => await db.Roles.FindAsync(roleId));

        public Task<Role> CreateRoleAsync(Role role) =>
            WithDbAsync(async db =>
            {
                db.Roles.Add(role);
                await db.SaveChangesAsync();
                return role;
            });

        public Task<Role?> UpdateRoleAsync(Role role) =>
            WithDbAsync(async db =>
            {
                var existing = await db.Roles.FindAsync(role.RoleID);
                if (existing is null)
                    return null;

                existing.RoleName = role.RoleName;

                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteRoleAsync(int roleId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Roles.FindAsync(roleId);
                if (entity is null)
                    return false;

                db.Roles.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
