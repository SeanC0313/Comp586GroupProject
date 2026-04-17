using System;
using System.Collections.Generic;
using System.Text;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Services;

namespace Comp586GroupProject.Views
{
    public partial class PatientsPage : ContentPage
    {
        private readonly PatientService _patientService;
        private List<Patient> _allPatients = new();

        public PatientsPage(IPatientInterface patientService)
        {
            InitializeComponent();
            _patientService = (PatientService)patientService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPatientsAsync();
        }

        private async Task LoadPatientsAsync()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            _allPatients = patients.ToList();
            PatientsList.ItemsSource = _allPatients;
        }

        private async void OnNewPatient(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewPatientPage));
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            var q = (e.NewTextValue ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(q))
            {
                PatientsList.ItemsSource = _allPatients;
                return;
            }

            PatientsList.ItemsSource = _allPatients.Where(p =>
                ($"{p.FirstName} {p.LastName}").ToLowerInvariant().Contains(q) ||
                (p.Email ?? "").ToLowerInvariant().Contains(q) ||
                (p.PhoneNumber ?? "").ToLowerInvariant().Contains(q)
            ).ToList();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.CurrentSelection.FirstOrDefault() as Patient;
            if (selected is null) 
                return;

            PatientsList.SelectedItem = null;

            await Shell.Current.GoToAsync($"{nameof(PatientDetailsPage)}?id={selected.PatientId}");
        }
    }
}
