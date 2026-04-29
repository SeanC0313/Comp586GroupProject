using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public class LabService : EfCoreServiceBase, ILabService
    {
        public LabService(IDbContextFactory<DatabaseContext> factory) : base(factory)
        {
        }

        // --- Lab Test Catalog ---

        public Task<IEnumerable<LabTest>> GetAllTestsAsync() =>
            WithDbAsync(async db => (await db.LabTests
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync()).AsEnumerable());

        public Task<LabTest> AddTestAsync(LabTest labTest) =>
            WithDbAsync(async db =>
            {
                db.LabTests.Add(labTest);
                await db.SaveChangesAsync();
                return labTest;
            });

        // --- Orders ---

        public Task<IEnumerable<LabOrder>> GetOrdersByPatientIdAsync(int patientId) =>
            WithDbAsync(async db => (await db.LabOrders
                .AsNoTracking()
                .Include(o => o.LabTest)
                .Include(o => o.Staff)
                .Include(o => o.Result)
                .Where(o => o.PatientId == patientId)
                .OrderByDescending(o => o.OrderedAt)
                .ToListAsync()).AsEnumerable());

        public Task<LabOrder?> GetOrderByIdAsync(int labOrderId) =>
            WithDbAsync(db => db.LabOrders
                .AsNoTracking()
                .Include(o => o.Patient)
                .Include(o => o.LabTest)
                .Include(o => o.Staff)
                .Include(o => o.Result)
                .FirstOrDefaultAsync(o => o.LabOrderId == labOrderId));

        public Task<LabOrder> PlaceOrderAsync(LabOrder order) =>
            WithDbAsync(async db =>
            {
                db.LabOrders.Add(order);
                await db.SaveChangesAsync();
                return order;
            });

        public Task<LabOrder?> UpdateOrderStatusAsync(int labOrderId, string status) =>
            WithDbAsync(async db =>
            {
                var existing = await db.LabOrders.FindAsync(labOrderId);
                if (existing is null)
                    return null;

                existing.Status = status;
                await db.SaveChangesAsync();
                return existing;
            });

        public Task<bool> DeleteOrderAsync(int labOrderId) =>
            WithDbAsync(async db =>
            {
                var entity = await db.LabOrders.FindAsync(labOrderId);
                if (entity is null)
                    return false;

                db.LabOrders.Remove(entity);
                await db.SaveChangesAsync();
                return true;
            });

        // --- Results ---

        public Task<LabResult?> GetResultByOrderIdAsync(int labOrderId) =>
            WithDbAsync(db => db.LabResults
                .AsNoTracking()
                .Include(r => r.ReviewedByStaff)
                .FirstOrDefaultAsync(r => r.LabOrderId == labOrderId));

        public Task<LabResult> RecordResultAsync(LabResult result) =>
            WithDbAsync(async db =>
            {
                db.LabResults.Add(result);

                // Auto-advance order status to Completed
                var order = await db.LabOrders.FindAsync(result.LabOrderId);
                if (order is not null)
                    order.Status = "Completed";

                await db.SaveChangesAsync();
                return result;
            });

        public Task<LabResult?> UpdateResultAsync(LabResult result) =>
            WithDbAsync(async db =>
            {
                var existing = await db.LabResults.FindAsync(result.LabResultId);
                if (existing is null)
                    return null;

                existing.ResultValue = result.ResultValue;
                existing.Unit = result.Unit;
                existing.IsAbnormal = result.IsAbnormal;
                existing.Notes = result.Notes;
                existing.ResultDate = result.ResultDate;
                existing.ReviewedByStaffId = result.ReviewedByStaffId;

                await db.SaveChangesAsync();
                return existing;
            });
    }
}
