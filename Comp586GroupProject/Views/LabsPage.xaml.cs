using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views
{
    public partial class LabsPage : ContentPage
    {
        private readonly ILabService _labService;

        public LabsPage(ILabService labService)
        {
            InitializeComponent();
            _labService = labService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTestsAsync();
        }

        private async Task LoadTestsAsync()
        {
            var tests = await _labService.GetAllTestsAsync();
            LabTestsList.ItemsSource = tests.ToList();
        }

        private async void OnLoadOrdersClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(PatientIdEntry.Text, out var patientId))
            {
                await DisplayAlert("Error", "Enter a valid Patient ID.", "OK");
                return;
            }

            var orders = await _labService.GetOrdersByPatientIdAsync(patientId);
            LabOrdersList.ItemsSource = orders.ToList();
        }

        private async void OnDeleteOrderClicked(object sender, EventArgs e)
        {
            if (sender is not Button button || button.BindingContext is not LabOrder order)
                return;

            await _labService.DeleteOrderAsync(order.LabOrderId);
            OnLoadOrdersClicked(sender, e);
        }
    }
}
