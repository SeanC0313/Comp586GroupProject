using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comp586GroupProject.Views
{
    public partial class NewPatientPage : ContentPage
    {
        private readonly IPatientInterface _patientService;
        public NewPatientPage(IPatientInterface patientService)
        {
            InitializeComponent();
            _patientService = patientService;
            Dob.Date = DateTime.Today.AddYears(-18);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var first = (FirstName.Text ?? "").Trim();
            var last = (LastName.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            {
                await DisplayAlert("Missing info", "First and last name are required.", "OK");
                return;
            }

            var p = new Patient
            {
                FirstName = first,
                LastName = last,
                DOB = Dob.Date ?? DateTime.Today,
                PhoneNumber = (Phone.Text ?? "").Trim(),
                Email = (Email.Text ?? "").Trim(),
                MedicalHistory = (MedicalHistory.Text ?? "").Trim()
            };

            PatientStore.Add(p);

            await DisplayAlert("Saved", "Patient registered.", "OK");
            await Shell.Current.GoToAsync(".."); // back
        }

        private async void OnCancelClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("..");
    }
}
