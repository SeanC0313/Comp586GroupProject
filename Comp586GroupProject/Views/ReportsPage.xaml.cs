using Comp586GroupProject.Interfaces;

namespace Comp586GroupProject.Views;

public partial class ReportsPage : ContentPage
{
    private readonly IPatientInterface _patientService;
    private readonly IAppointmentInterface _appointmentService;
    private readonly IStaffService _staffService;

    public ReportsPage(
        IPatientInterface patientService,
        IAppointmentInterface appointmentService,
        IStaffService staffService)
    {
        InitializeComponent();
        _patientService = patientService;
        _appointmentService = appointmentService;
        _staffService = staffService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadReportDataAsync();
    }

    private async Task LoadReportDataAsync()
    {
        try
        {
            var patients = (await _patientService.GetAllPatientsAsync()).ToList();
            var appointments = (await _appointmentService.GetAllAppointmentsAsync()).ToList();
            var staff = (await _staffService.GetAllAsync()).ToList();

            TotalPatientsLabel.Text = patients.Count.ToString();
            TotalAppointmentsLabel.Text = appointments.Count.ToString();
            TotalStaffLabel.Text = staff.Count.ToString();

            var today = DateTime.Today;
            var todaysAppointments = appointments.Count(a => a.AppointmentDate.Date == today);
            TodaysAppointmentsLabel.Text = todaysAppointments.ToString();

            var upcomingAppointments = appointments
                .Where(a => a.AppointmentDate >= DateTime.Now)
                .OrderBy(a => a.AppointmentDate)
                .Take(5)
                .ToList();

            UpcomingAppointmentsCollection.ItemsSource = upcomingAppointments;

            var staffByRole = staff
                .GroupBy(s => s.Role?.RoleName ?? "Unassigned")
                .Select(g => $"{g.Key}: {g.Count()}")
                .ToList();

            StaffByRoleCollection.ItemsSource = staffByRole;

            var appointmentStatusSummary = appointments
                .GroupBy(a => string.IsNullOrWhiteSpace(a.Status) ? "Unknown" : a.Status)
                .Select(g => $"{g.Key}: {g.Count()}")
                .ToList();

            AppointmentStatusCollection.ItemsSource = appointmentStatusSummary;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load reports: {ex.Message}", "OK");
        }
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await LoadReportDataAsync();
    }
}