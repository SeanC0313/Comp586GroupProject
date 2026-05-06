using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views
{
    public partial class BillingPage : ContentPage
    {
        private readonly IBillingService _billingService;

        public BillingPage(IBillingService billingService)
        {
            InitializeComponent();
            _billingService = billingService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadBillingsAsync();
        }

        private async Task LoadBillingsAsync()
        {
            var billings = await _billingService.GetAllBillingsAsync();
            BillingList.ItemsSource = billings.ToList();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is not Button button || button.BindingContext is not Billing billing)
                return;

            await _billingService.DeleteBillingAsync(billing.BillingId);
            await LoadBillingsAsync();
        }
    }
}
