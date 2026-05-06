using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class InsurancePage : ContentPage
{
    private readonly IInsuranceService _insuranceService;
    private List<Insurance> _allInsurance = new();

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
        _allInsurance = items.ToList();
        InsuranceList.ItemsSource = _allInsurance;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            InsuranceList.ItemsSource = _allInsurance;
            return;
        }

        InsuranceList.ItemsSource = _allInsurance
            .Where(i =>
                (i.ProviderName ?? "").ToLowerInvariant().Contains(q) ||
                (i.Phone ?? "").ToLowerInvariant().Contains(q) ||
                (i.CoverageDetails ?? "").ToLowerInvariant().Contains(q))
            .ToList();
    }

    private async void OnNewInsuranceClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewInsurancePage));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Insurance insurance)
            return;

        bool confirm = await DisplayAlert("Delete Insurance", $"Delete {insurance.ProviderName}?", "Delete", "Cancel");

        if (!confirm)
            return;

        await _insuranceService.DeleteInsuranceAsync(insurance.InsuranceId);
        await LoadInsuranceAsync();
    }
}