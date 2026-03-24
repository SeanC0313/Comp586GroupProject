using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp586GroupProject.Models
{
    public class Billing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillingId { get; set; }

        public int? PatientId { get; set; }
        public int? AppointmentId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public string InsuranceCovered { get; set; }

        public string PaymentStatus { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }
}