using System;
using System.Collections.ObjectModel;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Services
{
    public static class PatientStore
    {
        public static ObservableCollection<Patient> Patients { get; } = new();

        public static void LoadPatients(IEnumerable<Patient> patients)
        {
            Patients.Clear();
            foreach (var p in patients)
                Patients.Add(p);
        }

        public static void AddPatient(Patient patient)
        {
            Patients.Add(patient);
        }

        public static void RemovePatient(Patient patient)
        {
            Patients.Remove(patient);
        }
    }
}