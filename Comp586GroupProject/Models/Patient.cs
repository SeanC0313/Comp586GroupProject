using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [StringLength(1)]
        [Column(TypeName = "enum('M','F','O')")]
        public string? Gender { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        public int? InsuranceId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
    }
}