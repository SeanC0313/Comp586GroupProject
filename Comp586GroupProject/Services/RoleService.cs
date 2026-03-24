using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Services
{
    public class RoleService : IRoleService
    {
        private readonly DatabaseContext _context;

        public RoleService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            var existing = await _context.Roles.FindAsync(role.RoleID);
            if (existing == null) return null;

            existing.RoleName = role.RoleName;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}