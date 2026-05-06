using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

[QueryProperty(nameof(Id), "id")]
public partial class EditAppointmentPage : ContentPage
{
    private readonly IAppointmentInterface _appointmentService;
    private readonly IPatientInterface _patientService;
    private readonly IStaffService _staffService;

    private int _appointmentId;
    private Appointment? _appointment;
    private List<Patient> _patients = new();
    private List<Staff> _staffMembers = new();

    public string AppointmentIdQuery
    {
        set
        {
            if (int.TryParse(value, out var parsed))
                _appointmentId = parsed;
        }
    }

    public EditAppointmentPage(
        IAppointmentInterface appointmentService,
        IPatientInterface patientService,
        IStaffService staffService)
    {
        InitializeComponent();
        _appointmentService = appointmentService;
        _patientService = patientService;
        _staffService = staffService;

        StatusPicker.ItemsSource = new List<string> { "Scheduled", "Completed", "Canceled" };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            _patients = (await _patientService.GetAllPatientsAsync()).ToList();
            _staffMembers = (await _staffService.GetAllAsync()).ToList();
            _appointment = await _appointmentService.GetAppointmentByIdAsync(_appointmentId);

            if (_appointment is null)
            {
                await DisplayAlert("Error", "Appointment not found.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            PatientPicker.ItemsSource = _patients
                .Select(p => $"{p.PatientId} - {p.FirstName} {p.LastName}")
                .ToList();

            StaffPicker.ItemsSource = _staffMembers
                .Select(s => $"{s.StaffID} - {s.FirstName} {s.LastName}")
                .ToList();

            PatientPicker.SelectedIndex = _patients.FindIndex(p => p.PatientId == _appointment.PatientID);
            StaffPicker.SelectedIndex = _staffMembers.FindIndex(s => s.StaffID == _appointment.StaffID);
            AppointmentDatePicker.Date = _appointment.AppointmentDate.Date;
            AppointmentTimePicker.Time = _appointment.AppointmentDate.TimeOfDay;
            StatusPicker.SelectedItem = _appointment.Status;
            NotesEditor.Text = _appointment.Notes;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load appointment: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_appointment is null)
            return;

        try
        {
            _appointment.PatientID = _patients[PatientPicker.SelectedIndex].PatientId;
            _appointment.StaffID = _staffMembers[StaffPicker.SelectedIndex].StaffID;
            DateTime date = AppointmentDatePicker.Date ?? DateTime.Today;
            TimeSpan time = AppointmentTimePicker.Time ?? new TimeSpan(9, 0, 0);

            _appointment.AppointmentDate = date.Date.Add(time);
            _appointment.Status = StatusPicker.SelectedItem?.ToString();
            _appointment.Notes = NotesEditor.Text?.Trim();

            await _appointmentService.UpdateAppointmentAsync(_appointment);

            await DisplayAlert("Saved", "Appointment updated.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not update appointment: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}