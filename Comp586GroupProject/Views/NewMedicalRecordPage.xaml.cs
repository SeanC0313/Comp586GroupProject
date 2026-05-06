using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewMedicalRecordPage : ContentPage
{
    private readonly IMedicalRecordService _medicalRecordService;

    public NewMedicalRecordPage(IMedicalRecordService medicalRecordService)
    {
        InitializeComponent();
        _medicalRecordService = medicalRecordService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (!int.TryParse(PatientIdEntry.Text, out var patientId))
            {
                await DisplayAlert("Error", "Patient ID is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(DiagnosisEntry.Text))
            {
                await DisplayAlert("Error", "Diagnosis is required.", "OK");
                return;
            }

            var record = new MedicalRecord
            {
                PatientId = patientId,
                StaffId = int.TryParse(StaffIdEntry.Text, out var s) ? s : null,
                Diagnosis = DiagnosisEntry.Text.Trim(),
                Symptoms = SymptomsEditor.Text?.Trim() ?? "",
                Notes = NotesEditor.Text?.Trim() ?? ""
            };

            await _medicalRecordService.AddAsync(record);
            await DisplayAlert("Saved", "Medical record created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create medical record: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}