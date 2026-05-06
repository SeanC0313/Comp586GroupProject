using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models.Reports;

namespace Comp586GroupProject.Views
{
    public partial class FinancialReportsPage : ContentPage
    {
        private readonly IFinancialReportService _financialReportService;

        public FinancialReportsPage(IFinancialReportService financialReportService)
        {
            InitializeComponent();
            _financialReportService = financialReportService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadReportsAsync();
        }

        private async Task LoadReportsAsync()
        {
            DateTime? from = FromDatePicker.Date;
            DateTime? to = ToDatePicker.Date;

            BillingSummary summary = await _financialReportService.GetSummaryAsync(from, to);
            TotalBilledLabel.Text = $"Total Billed: {summary.TotalBilled:C}";
            TotalPaidLabel.Text = $"Total Paid: {summary.TotalPaid:C}";
            TotalOutstandingLabel.Text = $"Outstanding: {summary.TotalOutstanding:C}";
            BillCountLabel.Text = $"Bills: {summary.TotalBillCount}";
            StatusCountLabel.Text = $"Paid: {summary.PaidCount} | Unpaid: {summary.UnpaidCount} | Pending: {summary.PendingCount}";

            var patientReports = await _financialReportService.GetPatientBillingReportAsync();
            PatientBillingList.ItemsSource = patientReports.ToList();

            var outstanding = await _financialReportService.GetOutstandingBillsAsync();
            OutstandingBillsList.ItemsSource = outstanding.ToList();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await LoadReportsAsync();
        }
    }
}
