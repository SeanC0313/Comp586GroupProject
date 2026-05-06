using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewPatientPage : ContentPage
{
    private readonly IPatientInterface _patientService;

    public NewPatientPage(IPatientInterface patientService)
    {
        InitializeComponent();
        _patientService = patientService;

        DobPicker.Date = DateTime.Today.AddYears(-18);
        GenderPicker.ItemsSource = new List<string> { "M", "F", "O" };
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            var first = (FirstNameEntry.Text ?? "").Trim();
            var last = (LastNameEntry.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            {
                await DisplayAlert("Missing info", "First and last name are required.", "OK");
                return;
            }

            int? insuranceId = null;

            if (!string.IsNullOrWhiteSpace(InsuranceIdEntry.Text) &&
                int.TryParse(InsuranceIdEntry.Text.Trim(), out var parsedInsuranceId))
            {
                insuranceId = parsedInsuranceId;
            }

            var patient = new Patient
            {
                FirstName = first,
                LastName = last,
                DOB = DobPicker.Date,
                Gender = GenderPicker.SelectedItem?.ToString(),
                Phone = PhoneEntry.Text?.Trim(),
                Email = EmailEntry.Text?.Trim(),
                Address = AddressEntry.Text?.Trim(),
                MedicalHistory = MedicalHistoryEditor.Text?.Trim(),
                InsuranceId = insuranceId
            };

            await _patientService.AddPatientAsync(patient);

            await DisplayAlert("Saved", "Patient registered.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not save patient: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}