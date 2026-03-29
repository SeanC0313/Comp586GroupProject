namespace Comp486GroupProject;

public class InventoryItem : Supplier
{
    public int Id{ get; set; }
    public string Name{ get; set; }
    public int QuantityonHand{ get; set; }
    public DateTime ExpirationDate{ get; set; }

    public InventoryItem(int id, string name, string phone, string email)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
    }

    public static bool IsLowStock(int threshold)
    {
        if(QuantityonHand < threshold)
        {
            return true;
        }return false;

    }

    public void ReduceStock(int quantity)
    {
        QuantityonHand -= quantity;
    }

    public void Restock(int quantity)
    {
        QuantityonHand += quantity;
    }
}