using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class StaffPage : ContentPage
{
    private readonly IStaffService _staffService;
    private List<Staff> _allStaff = new();

    public StaffPage(IStaffService staffService)
    {
        InitializeComponent();
        _staffService = staffService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadStaffAsync();
    }

    private async Task LoadStaffAsync()
    {
        try
        {
            var staff = await _staffService.GetAllAsync();
            _allStaff = staff.ToList();

            StaffList.ItemsSource = _allStaff;
            EmptyStateLabel.IsVisible = _allStaff.Count == 0;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load staff: {ex.Message}", "OK");
        }
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            StaffList.ItemsSource = _allStaff;
            return;
        }

        StaffList.ItemsSource = _allStaff
            .Where(s =>
                ($"{s.FirstName} {s.LastName}").ToLowerInvariant().Contains(q) ||
                (s.Email ?? "").ToLowerInvariant().Contains(q) ||
                (s.Specialty ?? "").ToLowerInvariant().Contains(q) ||
                (s.Role?.RoleName ?? "").ToLowerInvariant().Contains(q))
            .ToList();
    }

    private async void OnNewStaffClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewStaffPage));
    }

    private async void OnEditStaffClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Staff staff)
            return;

        await Shell.Current.GoToAsync($"{nameof(EditStaffPage)}?id={staff.StaffID}");
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await LoadStaffAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Staff staff)
            return;

        var confirm = await DisplayAlert(
            "Delete Staff",
            $"Delete {staff.FirstName} {staff.LastName}?",
            "Delete",
            "Cancel");

        if (!confirm)
            return;

        try
        {
            await _staffService.DeleteStaffAsync(staff.StaffID);
            await LoadStaffAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not delete staff: {ex.Message}", "OK");
        }
    }
}