using System;
using System.Collections.Generic;
using System.Text;
using Comp586GroupProject.Services;

namespace Comp586GroupProject.Views
{
    [QueryProperty(nameof(Id), "id")]
    public partial class PatientDetailsPage : ContentPage
    {
        private int _id;
        public string Id
        {
            set
            {
                if (int.TryParse(value, out var parsed))
                    _id = parsed;
            }
        }

        public PatientDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var p = PatientStore.Patients.FirstOrDefault(x => x.Id == _id);
            if (p is null)
            {
                NameLabel.Text = "Patient not found";
                return;
            }

            NameLabel.Text = p.FullName;
            DobLabel.Text = $"DOB: {p.DateOfBirth:MMMM d, yyyy}";
            EmailLabel.Text = $"Email: {p.Email}";
            PhoneLabel.Text = $"Phone: {p.Phone}";
        }
    }
}
