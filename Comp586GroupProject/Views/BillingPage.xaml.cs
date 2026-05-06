using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class BillingPage : ContentPage
{
    private readonly IBillingService _billingService;
    private List<Billing> _allBilling = new();

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
        _allBilling = billings.ToList();
        BillingList.ItemsSource = _allBilling;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            BillingList.ItemsSource = _allBilling;
            return;
        }

        BillingList.ItemsSource = _allBilling
            .Where(b =>
                (b.PaymentStatus ?? "").ToLowerInvariant().Contains(q) ||
                b.PatientId.ToString().Contains(q) ||
                b.AppointmentId.ToString().Contains(q) ||
                b.Amount.ToString().Contains(q))
            .ToList();
    }

    private async void OnNewBillingClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewBillingPage));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Billing billing)
            return;

        bool confirm = await DisplayAlert("Delete Billing", $"Delete bill #{billing.BillingId}?", "Delete", "Cancel");

        if (!confirm)
            return;

        await _billingService.DeleteBillingAsync(billing.BillingId);
        await LoadBillingsAsync();
    }
}