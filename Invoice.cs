namespace Comp586GroupProject;

public class Invoice
{
    public int Id { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssuedAt { get; set; }

    public Invoice(int id, string status, decimal totalAmount, DateTime issuedAt)
    {
        Id = id;
        Status = status;
        TotalAmount = totalAmount;
        IssuedAt = issuedAt;
    }

    public void AddCharge(decimal amount)
    {
        TotalAmount += amount;
    }

    public void MarkAsPaid()
    {
        Status = "Paid";
    }

    public Boolean IsOutstanding()
    {
        return Status != "Paid";
    }

}
