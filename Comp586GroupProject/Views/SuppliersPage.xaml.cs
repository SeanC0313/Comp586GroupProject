using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class SuppliersPage : ContentPage
{
    private readonly ISupplierService _supplierService;
    private List<Supplier> _allSuppliers = new();

    public SuppliersPage(ISupplierService supplierService)
    {
        InitializeComponent();
        _supplierService = supplierService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSuppliersAsync();
    }

    private async Task LoadSuppliersAsync()
    {
        var suppliers = await _supplierService.GetAllAsync();
        _allSuppliers = suppliers.ToList();
        SuppliersList.ItemsSource = _allSuppliers;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs e)
    {
        var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(q))
        {
            SuppliersList.ItemsSource = _allSuppliers;
            return;
        }

        SuppliersList.ItemsSource = _allSuppliers
            .Where(s =>
                (s.Name ?? "").ToLowerInvariant().Contains(q) ||
                (s.ContactName ?? "").ToLowerInvariant().Contains(q) ||
                (s.Phone ?? "").ToLowerInvariant().Contains(q) ||
                (s.Email ?? "").ToLowerInvariant().Contains(q) ||
                (s.Address ?? "").ToLowerInvariant().Contains(q))
            .ToList();
    }

    private async void OnNewSupplierClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewSupplierPage));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not Supplier supplier)
            return;

        bool confirm = await DisplayAlert("Delete Supplier", $"Delete {supplier.Name}?", "Delete", "Cancel");

        if (!confirm)
            return;

        await _supplierService.DeleteAsync(supplier.SupplierId);
        await LoadSuppliersAsync();
    }
}