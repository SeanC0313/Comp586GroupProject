using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views
{
    public partial class PrescriptionsPage : ContentPage
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsPage(IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _prescriptionService = prescriptionService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPrescriptionsAsync();
        }

        private async Task LoadPrescriptionsAsync()
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
            PrescriptionsList.ItemsSource = prescriptions.ToList();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await LoadPrescriptionsAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is not Button button || button.BindingContext is not Prescription prescription)
            {
                return;
            }
            await _prescriptionService.DeletePrescriptionAsync(prescription.PrescriptionID);
            await LoadPrescriptionsAsync();
        }
    }
}
