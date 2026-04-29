using Comp586GroupProject.Models;

namespace Comp586GroupProject.Interfaces
{
    public interface ILabService
    {
        // Lab test catalog
        Task<IEnumerable<LabTest>> GetAllTestsAsync();
        Task<LabTest> AddTestAsync(LabTest labTest);

        // Orders
        Task<IEnumerable<LabOrder>> GetOrdersByPatientIdAsync(int patientId);
        Task<LabOrder?> GetOrderByIdAsync(int labOrderId);
        Task<LabOrder> PlaceOrderAsync(LabOrder order);
        Task<LabOrder?> UpdateOrderStatusAsync(int labOrderId, string status);
        Task<bool> DeleteOrderAsync(int labOrderId);

        // Results
        Task<LabResult?> GetResultByOrderIdAsync(int labOrderId);
        Task<LabResult> RecordResultAsync(LabResult result);
        Task<LabResult?> UpdateResultAsync(LabResult result);
    }
}
