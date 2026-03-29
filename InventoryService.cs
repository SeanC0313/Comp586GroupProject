using System.Runtime.CompilerServices;

namespace Comp486GroupProject;

public class InventoryService
{
    private List<InventoryItem> items = new List<InventoryItem>();
    public void DispenseMedication(int itemId, int quantity)
    {
        var item = items.FirstOrDefault(i => i.Id == itemId);
        if (item != null && item > 0)
        {
            item.ReduceStock(quantity);
        }
        else
        {
            Console.WriteLine("Out of Stock");
        }
    }

    public void RestockItem(int itemId, int quantity)
    {
        var item = items.FirstOrDefault(i => i.Id == itemId);
        item?.Restock(quantity);
        
    }

    public List<InventoryItem> GetLowStockItems()
    {
        return inventoryItems.Where(i => i.isLowStock()).ToList();

    }
}