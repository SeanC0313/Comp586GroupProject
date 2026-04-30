using Comp586GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int roleId);
    }
}