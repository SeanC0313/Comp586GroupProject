using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class MedicalRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicalRecordId { get; set; }

        public int PatientId { get; set; }

        // The staff member (doctor/nurse) who created this record
        public int? StaffId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Diagnosis { get; set; }

        public string Symptoms { get; set; }

        public string Notes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RecordedAt { get; set; }

        // Navigation properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }
    }
}
