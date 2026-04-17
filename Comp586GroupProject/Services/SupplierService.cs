using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class SupplierService : EfCoreServiceBase, ISupplierService
    {
        public SupplierService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        public Task<IEnumerable<Supplier>> GetAllAsync() =>
            WithDbAsync(async db => (await db.Suppliers
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .ToListAsync()).AsEnumerable());

        public Task<Supplier?> GetByIdAsync(int supplierId) =>
            WithDbAsync(db => db.Suppliers
                .AsNoTracking()
                .Include(s => s.Medications)
                .FirstOrDefaultAsync(s => s.SupplierId == supplierId));

        public Task<IEnumerable<Medication>> GetMedicationsBySupplierAsync(int supplierId) =>
            WithDbAsync(async db => (await db.Medications
                .AsNoTracking()
                .Where(m => m.SupplierId == supplierId)
                .OrderBy(m => m.Name)
                .ToListAsync()).AsEnumerable());

        public Task<Supplier> AddAsync(Supplier supplier) =>
            WithDbAsync(async db =>
            {
                db.Suppliers.Add(supplier);
                await db.SaveChangesAsync();
                return supplier;
            });

        public Task<Supplier?> UpdateAsync(Supplier supplier) =>
            WithDbAsync(async db =>
            {
                var existing = await db.Suppliers.FindAsync(supplier.SupplierId);
                if (existing is null)
                    return null;

                existing.Name = supplier.Name;
                existing.ContactName = supplier.ContactName;
                existing.Phone = supplier.Phone;
                existing.Email = supplier.Email;
                existing.Address = supplier.Address;

                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteAsync(int supplierId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.Suppliers.FindAsync(supplierId);
                if (entity is null)
                    return false;

                db.Suppliers.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });
    }
}
