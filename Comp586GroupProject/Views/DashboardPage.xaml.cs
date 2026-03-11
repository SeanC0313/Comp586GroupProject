using System;
using System.Collections.Generic;
using System.Text;

namespace Comp586GroupProject.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterPatient(object sender, EventArgs e)
            => await Shell.Current.GoToAsync(nameof(NewPatientPage));

        private async void OnViewPatients(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//patients");
        private async void OnAddPatientClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//patients"); // or your route
        }

        private async void OnViewAppointmentsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Stub", "Appointments page not added yet.", "OK");
        }

        private async void OnManageStaffClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Stub", "Staff page not added yet.", "OK");
        }

        private async void OnGenerateReportsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Stub", "Reports page not added yet.", "OK");
        }
    }
}
