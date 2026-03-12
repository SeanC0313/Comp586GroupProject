namespace Comp586GroupProject;

public class BillingService
{
    private List<Invoice> invoices = new List<Invoice>();


    public Invoice CreateInvoice(int patientId)
    {
        var invoice = new Invoice(0, "Pending", 0, DateTime.Now);
        invoices.Add(invoice);
        return invoice;
    }

    public void AddPayment(int invoiceId, decimal amount)
    {
        var invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (invoice != null)
        {
            invoice.AddCharge(-amount);
            if (invoice.TotalAmount <= 0)
            {
                invoice.MarkAsPaid();
            }
        }
    }

    public decimal CalculateOutstanding(int invoiceId)
    {
        var invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (invoice != null && invoice.IsOutstanding())
        {
            return invoice.TotalAmount;
        }
        return 0;
    }

    public List<Invoice> GetOutstandingInvoices()
    {
        return invoices.Where(i => i.IsOutstanding()).ToList();
    }

    public decimal GetTotalOutstandingAmount()
    {
        return invoices.Where(i => i.IsOutstanding()).Sum(i => i.TotalAmount);
    }
}