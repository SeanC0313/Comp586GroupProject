using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewLabTestPage : ContentPage
{
    private readonly ILabService _labService;

    public NewLabTestPage(ILabService labService)
    {
        InitializeComponent();
        _labService = labService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Test name is required.", "OK");
                return;
            }

            var test = new LabTest
            {
                Name = NameEntry.Text.Trim(),
                Description = DescriptionEditor.Text?.Trim() ?? "",
                ReferenceRange = ReferenceRangeEntry.Text?.Trim() ?? ""
            };

            await _labService.AddTestAsync(test);
            await DisplayAlert("Saved", "Lab test created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create lab test: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}