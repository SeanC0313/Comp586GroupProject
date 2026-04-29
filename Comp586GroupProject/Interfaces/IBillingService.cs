using Comp586GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Interfaces
{
    public interface IBillingService
    {
        Task<IEnumerable<Billing>> GetAllBillingsAsync();
        Task<Billing?> GetBillingByIdAsync(int billingId);
        Task<Billing> CreateBillingAsync(Billing billing);
        Task<Billing?> UpdateBillingAsync(Billing billing);
        Task<bool> DeleteBillingAsync(int billingId);
    }
}