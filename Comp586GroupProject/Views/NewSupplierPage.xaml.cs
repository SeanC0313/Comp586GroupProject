using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views;

public partial class NewSupplierPage : ContentPage
{
    private readonly ISupplierService _supplierService;

    public NewSupplierPage(ISupplierService supplierService)
    {
        InitializeComponent();
        _supplierService = supplierService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Supplier name is required.", "OK");
                return;
            }

            var supplier = new Supplier
            {
                Name = NameEntry.Text.Trim(),
                ContactName = ContactNameEntry.Text?.Trim() ?? "",
                Phone = PhoneEntry.Text?.Trim() ?? "",
                Email = EmailEntry.Text?.Trim() ?? "",
                Address = AddressEditor.Text?.Trim() ?? ""
            };

            await _supplierService.AddAsync(supplier);
            await DisplayAlert("Saved", "Supplier created.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not create supplier: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}