using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class Medication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicationId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }
    }
}