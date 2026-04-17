using System;
using System.Collections.Generic;
using System.Text;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Services;

namespace Comp586GroupProject.Views
{
    [QueryProperty(nameof(Id), "id")]
    public partial class PatientDetailsPage : ContentPage
    {
        private readonly IPatientInterface _patientService;
        private int _id;
        public string Id
        {
            set
            {
                if (int.TryParse(value, out var id))
                    _id = id;
            }
        }

        public PatientDetailsPage(IPatientInterface patientService)
        {
            InitializeComponent();
            _patientService = patientService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var p = await _patientService.GetPatientByIdAsync(_id);
            if (p == null)
            {
                await DisplayAlert("Error", "Patient not found.", "OK");
                return;
            }

            NameLabel.Text = $"{p.FirstName} {p.LastName}";
            DobLabel.Text = $"DOB: {p.DOB:MMMM d, yyyy}";
            EmailLabel.Text = $"Email: {p.Email}";
            PhoneLabel.Text = $"Phone: {p.PhoneNumber}";
        }
    }
}
