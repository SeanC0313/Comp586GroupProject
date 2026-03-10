using System;
using System.Collections.Generic;

namespace Comp586GroupProject.Models
{
    public class Patient
    {
        public int PatientId { get; set; }  // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }   // Date of Birth
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Insurance { get; set; }
        public string MedicalHistory { get; set; }  // optional: previous diagnoses, conditions
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}