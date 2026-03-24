using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class Insurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsuranceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProviderName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public string CoverageDetails { get; set; }
    }
}