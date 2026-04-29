using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Comp586GroupProject.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        [MaxLength(50)]
        public string RoleName { get; set; }

        // Navigation property
        public ICollection<Staff> StaffMembers { get; set; }
    }
}