using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        public int? PatientID { get; set; }
        public int? StaffID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Column(TypeName = "enum('Scheduled','Completed','Canceled')")]
        public string Status { get; set; } = "Scheduled";

        public string Notes { get; set; }

        // Navigation properties
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }
        [ForeignKey("StaffID")]
        public virtual Staff Staff { get; set; }
    }
}