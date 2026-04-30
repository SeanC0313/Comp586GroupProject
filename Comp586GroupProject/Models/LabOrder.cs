using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    // A lab test ordered for a specific patient
    public class LabOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabOrderId { get; set; }

        public int PatientId { get; set; }

        // Staff member who ordered the test
        public int? StaffId { get; set; }

        public int LabTestId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public string Notes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderedAt { get; set; }

        // Navigation properties
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }

        [ForeignKey("LabTestId")]
        public virtual LabTest LabTest { get; set; }

        public virtual LabResult Result { get; set; }
    }
}
