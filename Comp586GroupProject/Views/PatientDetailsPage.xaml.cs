using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

[QueryProperty(nameof(PatientIdQuery), "id")]
public partial class PatientDetailsPage : ContentPage
{
    private readonly IPatientInterface _patientService;
    private int _id;

    public string PatientIdQuery
    {
        set
        {
            if (int.TryParse(value, out var parsed))
                _id = parsed;
        }
    }

    public PatientDetailsPage(IPatientInterface patientService)
    {
        InitializeComponent();
        _patientService = patientService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var patient = await _patientService.GetPatientByIdAsync(_id);

            if (patient is null)
            {
                NameLabel.Text = "Patient not found";
                return;
            }

            NameLabel.Text = $"{patient.FirstName} {patient.LastName}";
            DobLabel.Text = patient.DOB.HasValue ? $"DOB: {patient.DOB.Value:MMMM d, yyyy}" : "DOB: Not provided";
            GenderLabel.Text = $"Gender: {patient.Gender}";
            PhoneLabel.Text = $"Phone: {patient.Phone}";
            EmailLabel.Text = $"Email: {patient.Email}";
            AddressLabel.Text = $"Address: {patient.Address}";
            InsuranceIdLabel.Text = patient.InsuranceId.HasValue
                ? $"Insurance ID: {patient.InsuranceId.Value}"
                : "Insurance ID: Not provided";
            MedicalHistoryLabel.Text = $"Medical History: {patient.MedicalHistory}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load patient: {ex.Message}", "OK");
        }
    }
    private async void OnMedicalRecordsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync($"{nameof(MedicalRecordsPage)}?patientId={_id}");

    private async void OnPrescriptionsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync($"{nameof(PrescriptionsPage)}?patientId={_id}");

    private async void OnLabsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync($"{nameof(LabsPage)}?patientId={_id}");

    private async void OnBillingClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync($"{nameof(BillingPage)}?patientId={_id}");
}