using System;
using UnityEngine;

public class ItemScript : MonoBehaviour, IInteractable
{   
    private GameObject itemObject;
    public InventoryEntry inventoryEntry;
    public ItemData itemData;
    private MeshRenderer meshRenderer;
    public GameObject ItemObject => itemObject;

    private void Awake()    
    {
        meshRenderer = GetComponent<MeshRenderer>();
        itemObject = gameObject;
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
     }

    public void Highlight()
    {
        
    }
}
