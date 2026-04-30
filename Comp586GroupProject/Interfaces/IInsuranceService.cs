using Comp586GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Interfaces
{
    public interface IInsuranceService
    {
        Task<IEnumerable<Insurance>> GetAllInsurancesAsync();
        Task<Insurance?> GetInsuranceByIdAsync(int insuranceId);
        Task<Insurance> CreateInsuranceAsync(Insurance insurance);
        Task<Insurance?> UpdateInsuranceAsync(Insurance insurance);
        Task<bool> DeleteInsuranceAsync(int insuranceId);
    }
}