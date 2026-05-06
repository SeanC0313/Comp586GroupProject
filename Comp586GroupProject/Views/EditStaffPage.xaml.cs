using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

[QueryProperty(nameof(StaffIdQuery), "id")]
public partial class EditStaffPage : ContentPage
{
    private readonly IStaffService _staffService;
    private Staff? _staff;
    private int _staffId;

    public string StaffIdQuery
    {
        set
        {
            if (int.TryParse(value, out var parsed))
                _staffId = parsed;
        }
    }

    public EditStaffPage(IStaffService staffService)
    {
        InitializeComponent();
        _staffService = staffService;

        RolePicker.ItemsSource = new List<string>
        {
            "1 - Admin",
            "2 - Doctor",
            "3 - Nurse",
            "4 - Billing"
        };
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
            _staff = await _staffService.GetByIdAsync(_staffId);

            if (_staff is null)
            {
                await DisplayAlert("Error", "Staff member not found.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            FirstNameEntry.Text = _staff.FirstName;
            LastNameEntry.Text = _staff.LastName;
            EmailEntry.Text = _staff.Email;
            SpecialtyEntry.Text = _staff.Specialty;
            RolePicker.SelectedItem = $"{_staff.RoleID} - {(_staff.Role?.RoleName ?? "Role")}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load staff member: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_staff is null)
            return;

        try
        {
            _staff.FirstName = FirstNameEntry.Text?.Trim();
            _staff.LastName = LastNameEntry.Text?.Trim();
            _staff.Email = EmailEntry.Text?.Trim();
            _staff.Specialty = SpecialtyEntry.Text?.Trim();

            var selectedRoleText = RolePicker.SelectedItem?.ToString() ?? "1 - Admin";
            var roleIdText = selectedRoleText.Split('-')[0].Trim();

            if (int.TryParse(roleIdText, out var roleId))
                _staff.RoleID = roleId;

            await _staffService.UpdateStaffAsync(_staff);

            await DisplayAlert("Saved", "Staff member updated.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not update staff member: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}