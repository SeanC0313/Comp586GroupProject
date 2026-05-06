using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class RoleManagementPage : ContentPage
{
    private readonly IRoleService _roleService;
    private List<Role> _allRoles = new();

    public RoleManagementPage(IRoleService roleService)
    {
        InitializeComponent();
        _roleService = roleService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadRolesAsync();
    }

    private async Task LoadRolesAsync()
    {
        var roles = await _roleService.GetAllRolesAsync();
        _allRoles = roles.ToList();
        RolesList.ItemsSource = _allRoles;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            RolesList.ItemsSource = _allRoles;
            return;
        }

        RolesList.ItemsSource = _allRoles
            .Where(r => (r.RoleName ?? "").ToLowerInvariant().Contains(q))
            .ToList();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var roleName = RoleNameEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(roleName))
        {
            await DisplayAlert("Error", "Enter a role name.", "OK");
            return;
        }

        await _roleService.CreateRoleAsync(new Role { RoleName = roleName });
        RoleNameEntry.Text = "";
        await LoadRolesAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Role role)
            return;

        bool confirm = await DisplayAlert("Delete Role",$"Delete role {role.RoleName}?","Delete","Cancel");

        if (!confirm)
            return;

        await _roleService.DeleteRoleAsync(role.RoleID);
        await LoadRolesAsync();
    }
}