using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        public int? PatientID { get; set; }
        public int? StaffID { get; set; }
        public int? MedicationID { get; set; }

        [Required, MaxLength(20)]
        public string Dosage { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        // Navigation properties
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }
        [ForeignKey("StaffID")]
        public virtual Staff Staff { get; set; }
        [ForeignKey("MedicationID")]
        public virtual Medication Medication { get; set; }
        // You can create a Medication entity later if needed
        // public virtual Medication Medication { get; set; }
    }
}