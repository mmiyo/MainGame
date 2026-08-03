using System;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{   
    private InventoryEntry inventoryEntry;
    public ItemData itemData;
    private MeshRenderer meshRenderer;
    private int count;

    private void Awake()    
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Start()
    {
    }
    public void Interaction(PlayerManager player)
    {   
        inventoryEntry = new();
        player.inventoryManager.AddToInventory(inventoryEntry.entry(itemData));
        Destroy(gameObject);
        count++;
    }

    public void Highlight()
    {
        
    }
}
