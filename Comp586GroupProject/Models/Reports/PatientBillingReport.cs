namespace Comp586GroupProject.Models.Reports
{
    public class PatientBillingReport
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public decimal TotalBilled { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalOutstanding { get; set; }
        public int BillCount { get; set; }
    }
}
