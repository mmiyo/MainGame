using UnityEngine;

public class InventoryEntry 
{   
    public ItemData data;
    private InventoryManager inventoryManager;
    public InventoryEntry entry(ItemData itemData)
    {   
        data = itemData;
        return this;
    }
}
