using System;

namespace Comp586GroupProject.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }  // Primary Key
        public int? PatientID { get; set; }     // Foreign Key
        public int? StaffID { get; set; }       // Foreign Key
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; } = "Scheduled"; // Default status
        public string Notes { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Staff Staff { get; set; }
    }
}