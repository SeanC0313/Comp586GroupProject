using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    // The recorded result for a completed lab order
    public class LabResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabResultId { get; set; }

        public int LabOrderId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ResultValue { get; set; }

        // e.g. "mg/dL", "mmol/L"
        [MaxLength(50)]
        public string Unit { get; set; }

        public bool IsAbnormal { get; set; } = false;

        public string Notes { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ResultDate { get; set; }

        // Staff member who reviewed/entered the result
        public int? ReviewedByStaffId { get; set; }

        // Navigation properties
        [ForeignKey("LabOrderId")]
        public virtual LabOrder LabOrder { get; set; }

        [ForeignKey("ReviewedByStaffId")]
        public virtual Staff ReviewedByStaff { get; set; }
    }
}
