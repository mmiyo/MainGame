using UnityEngine;

[System.Serializable]
public class InventoryEntry 
{   
    public ItemData data;
    public InventoryEntry Entry(ItemData itemData)
    {   
        data = itemData;
        return this;
    }
}
