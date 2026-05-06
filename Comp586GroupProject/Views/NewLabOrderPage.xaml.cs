using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewLabOrderPage : ContentPage
{
    private readonly ILabService _labService;

    public NewLabOrderPage(ILabService labService)
    {
        InitializeComponent();
        _labService = labService;

        StatusPicker.ItemsSource = new List<string> { "Pending", "Completed", "Canceled" };
        StatusPicker.SelectedIndex = 0;
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

            if (!int.TryParse(LabTestIdEntry.Text, out var labTestId))
            {
                await DisplayAlert("Error", "Lab Test ID is required.", "OK");
                return;
            }

            var order = new LabOrder
            {
                PatientId = patientId,
                StaffId = int.TryParse(StaffIdEntry.Text, out var s) ? s : null,
                LabTestId = labTestId,
                Status = StatusPicker.SelectedItem?.ToString() ?? "Pending",
                Notes = NotesEditor.Text?.Trim() ?? ""
            };

            await _labService.PlaceOrderAsync(order);
            await DisplayAlert("Saved", "Lab order created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create lab order: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}