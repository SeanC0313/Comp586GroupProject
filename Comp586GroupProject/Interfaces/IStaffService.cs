using Comp586GroupProject.Models;

namespace Comp586GroupProject.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Staff?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<Staff?> GetByIdAsync(int staffId, CancellationToken cancellationToken = default);
        Task<Staff> AddStaffAsync(Staff staff, string plainPassword, CancellationToken cancellationToken = default);
        Task<Staff?> UpdateStaffAsync(Staff staff, CancellationToken cancellationToken = default);
        Task<bool> DeleteStaffAsync(int staffId, CancellationToken cancellationToken = default);
    }
}
