using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views
{
    public partial class MedicalRecordsPage : ContentPage
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordsPage(IMedicalRecordService medicalRecordService)
        {
            InitializeComponent();
            _medicalRecordService = medicalRecordService;
        }

        private async void OnLoadClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(PatientIdEntry.Text, out int patientId))
            {
                await DisplayAlert("Error", "Please enter a valid patient ID.", "OK");
                return;
            }

            var records = await _medicalRecordService.GetByPatientIdAsync(patientId);
            RecordsList.ItemsSource = records.ToList();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is not Button button || button.BindingContext is not MedicalRecord record)
            {
                return;
            }

            await _medicalRecordService.DeleteAsync(record.MedicalRecordId);
            OnLoadClicked(sender, e);
        }
    }
}
