using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Comp586GroupProject.Models;

namespace Comp586GroupProject.Services
{
    public static class PatientStore
    {
        // ObservableCollection updates the UI automatically
        public static ObservableCollection<Patient> Patients { get; } = new()
    {
        new Patient { Id = 1, FirstName = "Alex", LastName = "Rivera", DateOfBirth = new DateTime(1998, 5, 12), Phone="555-1234", Email="alex@example.com" },
        new Patient { Id = 2, FirstName = "Sam", LastName = "Nguyen", DateOfBirth = new DateTime(1987, 9, 3), Phone="555-9981", Email="sam@example.com" },
    };

        private static int _nextId = 3;

        public static Patient Add(Patient p)
        {
            p.Id = _nextId++;
            Patients.Add(p);
            return p;
        }
    }
}
