using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewStaffPage : ContentPage
{
    private readonly IStaffService _staffService;

    public NewStaffPage(IStaffService staffService)
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

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            var firstName = (FirstNameEntry.Text ?? "").Trim();
            var lastName = (LastNameEntry.Text ?? "").Trim();
            var email = (EmailEntry.Text ?? "").Trim();
            var specialty = (SpecialtyEntry.Text ?? "").Trim();
            var password = PasswordEntry.Text ?? "";

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Missing info", "First name, last name, email, and password are required.", "OK");
                return;
            }

            if (RolePicker.SelectedIndex < 0)
            {
                await DisplayAlert("Missing info", "Select a role.", "OK");
                return;
            }

            var selectedRoleText = RolePicker.SelectedItem?.ToString() ?? "1 - Admin";
            var roleIdText = selectedRoleText.Split('-')[0].Trim();

            if (!int.TryParse(roleIdText, out var roleId))
            {
                await DisplayAlert("Error", "Invalid role selected.", "OK");
                return;
            }

            var staff = new Staff
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Specialty = specialty,
                RoleID = roleId
            };

            await _staffService.AddStaffAsync(staff, password);

            await DisplayAlert("Saved", "Staff member created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not save staff member: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}