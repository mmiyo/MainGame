using UnityEngine;

[System.Serializable]
public class InventoryEntry 
{   
    public ItemData data;
    public int itemCount = 1;
    public InventoryEntry Entry(ItemData itemData)
    {   
        data = itemData;
        return this;
    }
}
