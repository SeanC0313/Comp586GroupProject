using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewPrescriptionPage : ContentPage
{
    private readonly IPrescriptionService _prescriptionService;

    public NewPrescriptionPage(IPrescriptionService prescriptionService)
    {
        InitializeComponent();
        _prescriptionService = prescriptionService;

        StartDatePicker.Date = DateTime.Today;
        EndDatePicker.Date = DateTime.Today.AddDays(30);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(DosageEntry.Text))
            {
                await DisplayAlert("Error", "Dosage is required.", "OK");
                return;
            }

            var prescription = new Prescription
            {
                PatientID = int.TryParse(PatientIdEntry.Text, out var p) ? p : null,
                StaffID = int.TryParse(StaffIdEntry.Text, out var s) ? s : null,
                MedicationID = int.TryParse(MedicationIdEntry.Text, out var m) ? m : null,
                Dosage = DosageEntry.Text.Trim(),
                StartDate = StartDatePicker.Date ?? DateTime.Today,
                EndDate = EndDatePicker.Date
            };

            await _prescriptionService.CreatePrescriptionAsync(prescription);
            await DisplayAlert("Saved", "Prescription created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create prescription: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}