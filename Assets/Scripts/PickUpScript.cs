using System;
using UnityEngine;

public class PickUpScript : MonoBehaviour, IInteractable
{   
    public ItemData itemData;
    private MeshRenderer meshRenderer;

    private void Awake()    
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Start()
    {
    }
    public void Interaction(PlayerManager player)
    {   
        //Debug.Log("gng");
        //if(inventoryManager)
        player.inventoryManager.AddToInventory(itemData, player);
        Destroy(gameObject);
    }

    public void Highlight()
    {
        
    }
}
