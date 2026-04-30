using Comp586GroupProject.Models;

namespace Comp586GroupProject.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int supplierId);
        Task<IEnumerable<Medication>> GetMedicationsBySupplierAsync(int supplierId);
        Task<Supplier> AddAsync(Supplier supplier);
        Task<Supplier?> UpdateAsync(Supplier supplier);
        Task<bool> DeleteAsync(int supplierId);
    }
}
