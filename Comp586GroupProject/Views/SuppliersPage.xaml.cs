using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Views
{
    public partial class SuppliersPage : ContentPage
    {
        private readonly ISupplierService _supplierService;

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
            SuppliersList.ItemsSource = suppliers.ToList();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is not Button button || button.BindingContext is not Supplier supplier)
                return;

            await _supplierService.DeleteAsync(supplier.SupplierId);
            await LoadSuppliersAsync();
        }
    }
}
