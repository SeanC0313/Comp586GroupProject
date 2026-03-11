using System;
using System.Collections.Generic;
using System.Text;

namespace Comp586GroupProject.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-18);
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
