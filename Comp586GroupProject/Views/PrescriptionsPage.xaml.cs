using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class PrescriptionsPage : ContentPage
{
    private readonly IPrescriptionService _prescriptionService;
    private List<Prescription> _allPrescriptions = new();

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
        _allPrescriptions = prescriptions.ToList();
        PrescriptionsList.ItemsSource = _allPrescriptions;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            PrescriptionsList.ItemsSource = _allPrescriptions;
            return;
        }

        PrescriptionsList.ItemsSource = _allPrescriptions
            .Where(p =>
                (p.Dosage ?? "").ToLowerInvariant().Contains(q) ||
                p.PatientID.ToString().Contains(q) ||
                p.StaffID.ToString().Contains(q) ||
                p.MedicationID.ToString().Contains(q))
            .ToList();
    }

    private async void OnNewPrescriptionClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewPrescriptionPage));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Prescription prescription)
            return;

        bool confirm = await DisplayAlert(
            "Delete Prescription",
            $"Delete prescription #{prescription.PrescriptionID}?",
            "Delete",
            "Cancel");

        if (!confirm)
            return;

        await _prescriptionService.DeletePrescriptionAsync(prescription.PrescriptionID);
        await LoadPrescriptionsAsync();
    }
}