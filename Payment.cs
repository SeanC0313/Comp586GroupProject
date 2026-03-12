namespace Comp586GroupProject;
public class Payment:Invoice
{
    public int Id { get; set; }
    public string Method { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }

    public Payment(int id, string method, decimal amount, DateTime paidAt)
    {
        Id = id;
        Method = method;
        Amount = amount;
        PaidAt = paidAt;
    }
}