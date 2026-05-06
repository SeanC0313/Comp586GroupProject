using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class PatientsPage : ContentPage
{
    private readonly IPatientInterface _patientService;
    private List<Patient> _allPatients = new();

    public PatientsPage(IPatientInterface patientService)
    {
        InitializeComponent();
        _patientService = patientService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadPatientsAsync();
    }

    private async Task LoadPatientsAsync()
    {
        try
        {
            var patients = await _patientService.GetAllPatientsAsync();
            _allPatients = patients.ToList();
            PatientsList.ItemsSource = _allPatients;
            EmptyStateLabel.IsVisible = _allPatients.Count == 0;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load patients: {ex.Message}", "OK");
        }
    }

    private async void OnNewPatient(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewPatientPage));
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            PatientsList.ItemsSource = _allPatients;
            return;
        }

        PatientsList.ItemsSource = _allPatients
            .Where(p =>
                $"{p.FirstName} {p.LastName}".ToLowerInvariant().Contains(q) ||
                (p.Phone ?? "").ToLowerInvariant().Contains(q) ||
                (p.Address ?? "").ToLowerInvariant().Contains(q))
            .ToList();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as Patient;
        if (selected is null)
            return;

        PatientsList.SelectedItem = null;
        await Shell.Current.GoToAsync($"{nameof(PatientDetailsPage)}?id={selected.PatientId}");
    }

    private async void OnEditPatientClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is not Button button || button.BindingContext is not Patient patient)
                return;
            await Shell.Current.GoToAsync($"{nameof(EditPatientPage)}?id={patient.PatientId}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not navigate to edit page: {ex.Message}", "OK");
        }
    }

    private async void OnDeletePatientClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Patient patient)
            return;
        bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {patient.FirstName} {patient.LastName}?", "Delete", "Cancel");
        if (!confirm)
            return;
        try
        {
            await _patientService.DeletePatientAsync(patient.PatientId);
            await LoadPatientsAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not delete patient: {ex.Message}", "OK");
        }
    }
}