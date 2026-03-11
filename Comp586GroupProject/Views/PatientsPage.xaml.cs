using System;
using System.Collections.Generic;
using System.Text;
using Comp586GroupProject.Models;
using Comp586GroupProject.Services;

namespace Comp586GroupProject.Views
{
    public partial class PatientsPage : ContentPage
    {
        private List<Patient> _all = new();

        public PatientsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _all = PatientStore.Patients.ToList();
            PatientsList.ItemsSource = _all;
        }

        private async void OnNewPatient(object sender, EventArgs e)
            => await Shell.Current.GoToAsync(nameof(NewPatientPage));

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(q))
            {
                PatientsList.ItemsSource = _all;
                return;
            }

            PatientsList.ItemsSource = _all.Where(p =>
                p.FullName.ToLowerInvariant().Contains(q) ||
                p.Email.ToLowerInvariant().Contains(q) ||
                p.Phone.ToLowerInvariant().Contains(q)
            ).ToList();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.CurrentSelection?.FirstOrDefault() as Patient;
            if (selected is null) return;

            // Clear selection so user can tap again later
            PatientsList.SelectedItem = null;

            await Shell.Current.GoToAsync($"{nameof(PatientDetailsPage)}?id={selected.Id}");
        }
    }
}
