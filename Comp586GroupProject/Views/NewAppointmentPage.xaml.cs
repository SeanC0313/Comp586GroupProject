using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewAppointmentPage : ContentPage
{
    private readonly IAppointmentInterface _appointmentService;
    private readonly IPatientInterface _patientService;
    private readonly IStaffService _staffService;

    private List<Patient> _patients = new();
    private List<Staff> _staffMembers = new();

    public NewAppointmentPage(
        IAppointmentInterface appointmentService,
        IPatientInterface patientService,
        IStaffService staffService)
    {
        InitializeComponent();

        _appointmentService = appointmentService;
        _patientService = patientService;
        _staffService = staffService;

        StatusPicker.ItemsSource = new List<string> { "Scheduled", "Completed", "Canceled" };
        StatusPicker.SelectedIndex = 0;

        AppointmentDatePicker.Date = DateTime.Today;
        AppointmentTimePicker.Time = new TimeSpan(9, 0, 0);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadFormDataAsync();
    }

    private async Task LoadFormDataAsync()
    {
        try
        {
            _patients = (await _patientService.GetAllPatientsAsync()).ToList();
            _staffMembers = (await _staffService.GetAllAsync()).ToList();

            PatientPicker.ItemsSource = _patients
                .Select(p => $"{p.PatientId} - {p.FirstName} {p.LastName}")
                .ToList();

            StaffPicker.ItemsSource = _staffMembers
                .Select(s => $"{s.StaffID} - {s.FirstName} {s.LastName}")
                .ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load patients/staff: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (PatientPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Missing info", "Select a patient.", "OK");
                return;
            }

            if (StaffPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Missing info", "Select a staff member.", "OK");
                return;
            }

            if (StatusPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Missing info", "Select a status.", "OK");
                return;
            }

            var selectedPatient = _patients[PatientPicker.SelectedIndex];
            var selectedStaff = _staffMembers[StaffPicker.SelectedIndex];

            DateTime date = AppointmentDatePicker.Date ?? DateTime.Today;
            TimeSpan time = AppointmentTimePicker.Time ?? new TimeSpan(9, 0, 0);

            DateTime appointmentDateTime = date.Date.Add(time);
            var appointment = new Appointment
            {
                PatientID = selectedPatient.PatientId,
                StaffID = selectedStaff.StaffID,
                AppointmentDate = appointmentDateTime,
                Status = StatusPicker.SelectedItem?.ToString() ?? "Scheduled",
                Notes = NotesEditor.Text?.Trim()
            };

            await _appointmentService.AddAppointmentAsync(appointment);

            await DisplayAlert("Saved", "Appointment created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not save appointment: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}