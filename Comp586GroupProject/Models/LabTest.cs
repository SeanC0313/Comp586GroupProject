using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    // Catalog of available lab test types
    public class LabTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabTestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        // e.g. "70-100 mg/dL" — shown alongside results
        [MaxLength(100)]
        public string ReferenceRange { get; set; }
    }
}
