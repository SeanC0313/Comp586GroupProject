using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Comp586GroupProject.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public int? RoleID { get; set; }

        [MaxLength(100)]
        public string Specialty { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string PassWordHash { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}