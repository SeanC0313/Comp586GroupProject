using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views
{
    public partial class InsurancePage : ContentPage
    {
        private readonly IInsuranceService _insuranceService;

        public InsurancePage(IInsuranceService insuranceService)
        {
            InitializeComponent();
            _insuranceService = insuranceService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadInsuranceAsync();
        }

        private async Task LoadInsuranceAsync()
        {
            var items = await _insuranceService.GetAllInsurancesAsync();
            InsuranceList.ItemsSource = items.ToList();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is not Button button || button.BindingContext is not Insurance insurance)
                return;

            await _insuranceService.DeleteInsuranceAsync(insurance.InsuranceId);
            await LoadInsuranceAsync();
        }
    }
}
