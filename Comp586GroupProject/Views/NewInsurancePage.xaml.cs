using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewInsurancePage : ContentPage
{
    private readonly IInsuranceService _insuranceService;

    public NewInsurancePage(IInsuranceService insuranceService)
    {
        InitializeComponent();
        _insuranceService = insuranceService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ProviderNameEntry.Text))
            {
                await DisplayAlert("Error", "Provider name is required.", "OK");
                return;
            }

            var insurance = new Insurance
            {
                ProviderName = ProviderNameEntry.Text.Trim(),
                Phone = PhoneEntry.Text?.Trim() ?? "",
                CoverageDetails = CoverageDetailsEditor.Text?.Trim() ?? ""
            };

            await _insuranceService.CreateInsuranceAsync(insurance);
            await DisplayAlert("Saved", "Insurance provider created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create insurance: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}