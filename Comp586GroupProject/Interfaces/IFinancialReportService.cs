using Comp586GroupProject.Models;
using Comp586GroupProject.Models.Reports;

namespace Comp586GroupProject.Interfaces
{
    public interface IFinancialReportService
    {
        // Overall billing summary, optionally filtered by date range
        Task<BillingSummary> GetSummaryAsync(DateTime? from = null, DateTime? to = null);

        // All bills filtered by payment status (e.g. "Paid", "Unpaid", "Pending")
        Task<IEnumerable<Billing>> GetByPaymentStatusAsync(string status);

        // All bills for a specific patient
        Task<IEnumerable<Billing>> GetByPatientAsync(int patientId);

        // Per-patient breakdown of billing totals
        Task<IEnumerable<PatientBillingReport>> GetPatientBillingReportAsync();

        // All bills with outstanding balances
        Task<IEnumerable<Billing>> GetOutstandingBillsAsync();
    }
}
