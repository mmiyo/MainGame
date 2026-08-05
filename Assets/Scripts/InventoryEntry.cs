using UnityEngine;

[System.Serializable]
public class InventoryEntry 
{   
    public ItemData data;
    public int itemCount;
    public InventoryEntry Entry(ItemData itemData)
    {   
        data = itemData;
        return this;
    }
}
