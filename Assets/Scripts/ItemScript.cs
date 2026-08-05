using System;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{   
    public InventoryEntry inventoryEntry;
    public ItemData itemData;
    private MeshRenderer meshRenderer;
    public GameObject item;
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
        if(inventoryEntry == null)
        {
            inventoryEntry = new();
            inventoryEntry.Entry(itemData);
        }
        
        
        Debug.Log("Itemscript interaction function " + inventoryEntry.GetHashCode());


        player.inventoryManager.AddToInventory(inventoryEntry);
        Destroy(gameObject);
        count++;
    }

    public void Highlight()
    {
        
    }
}
