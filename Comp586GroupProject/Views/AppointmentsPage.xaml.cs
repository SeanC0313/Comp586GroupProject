using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class AppointmentsPage : ContentPage
{
    private readonly IAppointmentInterface _appointmentService;
    private List<Appointment> _allAppointments = new();

    public AppointmentsPage(IAppointmentInterface appointmentService)
    {
        InitializeComponent();
        _appointmentService = appointmentService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAppointmentsAsync();
    }

    private async Task LoadAppointmentsAsync()
    {
        try
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            _allAppointments = appointments
                .OrderBy(a => a.AppointmentDate)
                .ToList();

            AppointmentsList.ItemsSource = _allAppointments;
            EmptyStateLabel.IsVisible = _allAppointments.Count == 0;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load appointments: {ex.Message}", "OK");
        }
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            AppointmentsList.ItemsSource = _allAppointments;
            return;
        }

        AppointmentsList.ItemsSource = _allAppointments
            .Where(a =>
                (a.Status ?? "").ToLowerInvariant().Contains(q) ||
                (a.Notes ?? "").ToLowerInvariant().Contains(q) ||
                ($"{a.Patient?.FirstName} {a.Patient?.LastName}").ToLowerInvariant().Contains(q) ||
                ($"{a.Staff?.FirstName} {a.Staff?.LastName}").ToLowerInvariant().Contains(q))
            .ToList();
    }

    private async void OnNewAppointmentClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewAppointmentPage));
    }

    private async void OnEditAppointmentClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Appointment appointment)
            return;

        await Shell.Current.GoToAsync($"{nameof(EditAppointmentPage)}?id={appointment.AppointmentID}");
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await LoadAppointmentsAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Appointment appointment)
            return;

        var confirm = await DisplayAlert(
            "Delete Appointment",
            $"Delete appointment #{appointment.AppointmentID}?",
            "Delete",
            "Cancel");

        if (!confirm)
            return;

        try
        {
            await _appointmentService.DeleteAppointmentAsync(appointment.AppointmentID);
            await LoadAppointmentsAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not delete appointment: {ex.Message}", "OK");
        }
    }
}