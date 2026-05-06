using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewBillingPage : ContentPage
{
    private readonly IBillingService _billingService;

    public NewBillingPage(IBillingService billingService)
    {
        InitializeComponent();
        _billingService = billingService;

        PaymentStatusPicker.ItemsSource = new List<string> { "Pending", "Paid", "Unpaid" };
        PaymentStatusPicker.SelectedIndex = 0;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            int? patientId = int.TryParse(PatientIdEntry.Text, out var p) ? p : null;
            int? appointmentId = int.TryParse(AppointmentIdEntry.Text, out var a) ? a : null;

            if (!decimal.TryParse(AmountEntry.Text, out var amount))
            {
                await DisplayAlert("Error", "Enter a valid amount.", "OK");
                return;
            }

            var billing = new Billing
            {
                PatientId = patientId,
                AppointmentId = appointmentId,
                Amount = amount,
                InsuranceCovered = InsuranceCoveredEntry.Text?.Trim() ?? "",
                PaymentStatus = PaymentStatusPicker.SelectedItem?.ToString() ?? "Pending"
            };

            await _billingService.CreateBillingAsync(billing);
            await DisplayAlert("Saved", "Billing record created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create billing: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}