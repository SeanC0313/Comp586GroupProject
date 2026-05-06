using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

[QueryProperty(nameof(PatientIdQuery), "id")]
public partial class EditPatientPage : ContentPage
{
    private readonly IPatientInterface _patientService;
    private int _patientId;
    private Patient? _patient;

    public string PatientIdQuery
    {
        set
        {
            if (int.TryParse(value, out var parsed))
            {
                _patientId = parsed;
                MainThread.BeginInvokeOnMainThread(async () => await LoadPatientAsync());
            }
        }
    }

    public EditPatientPage(IPatientInterface patientService)
    {
        InitializeComponent();
        _patientService = patientService;

        GenderPicker.ItemsSource = new List<string> { "M", "F", "O" };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }

    private async Task LoadPatientAsync()
    {
        await DisplayAlert("Debug", $"Loading patient with ID: {_patientId}", "OK");
        try
        {
            _patient = await _patientService.GetPatientByIdAsync(_patientId);

            if (_patient is null)
            {
                await DisplayAlert("Error", "Patient not found.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            FirstNameEntry.Text = _patient.FirstName;
            LastNameEntry.Text = _patient.LastName;
            DobPicker.Date = _patient.DOB ?? DateTime.Today;
            GenderPicker.SelectedItem = _patient.Gender;
            PhoneEntry.Text = _patient.Phone;
            AddressEntry.Text = _patient.Address;
            EmailEntry.Text = _patient.Email;
            MedicalHistoryEditor.Text = _patient.MedicalHistory;
            InsuranceIdEntry.Text = _patient.InsuranceId?.ToString();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load patient: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_patient is null)
            return;

        try
        {
            int? insuranceId = null;

            if (!string.IsNullOrWhiteSpace(InsuranceIdEntry.Text) &&
                int.TryParse(InsuranceIdEntry.Text.Trim(), out var parsedInsuranceId))
            {
                insuranceId = parsedInsuranceId;
            }

            _patient.FirstName = FirstNameEntry.Text?.Trim();
            _patient.LastName = LastNameEntry.Text?.Trim();
            _patient.DOB = DobPicker.Date;
            _patient.Gender = GenderPicker.SelectedItem?.ToString();
            _patient.Phone = PhoneEntry.Text?.Trim();
            _patient.Email = EmailEntry.Text?.Trim();
            _patient.Address = AddressEntry.Text?.Trim();
            _patient.MedicalHistory = MedicalHistoryEditor.Text?.Trim();
            _patient.InsuranceId = insuranceId;

            await _patientService.UpdatePatientAsync(_patient);

            await DisplayAlert("Saved", "Patient updated.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not update patient: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}