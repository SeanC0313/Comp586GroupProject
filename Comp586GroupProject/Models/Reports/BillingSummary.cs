namespace Comp586GroupProject.Models.Reports
{
    public class BillingSummary
    {
        public decimal TotalBilled { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalOutstanding { get; set; }
        public int TotalBillCount { get; set; }
        public int PaidCount { get; set; }
        public int UnpaidCount { get; set; }
        public int PendingCount { get; set; }
    }
}
