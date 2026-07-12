using System;
using UnityEngine;

public class PickUpScript : MonoBehaviour, IInteractable 
{   
    public ItemData itemData;
    private void Awake()
    {
    }
    public void Interaction(PlayerManager player)
    {   
        //Debug.Log("gng");
        //if(inventoryManager)
        player.inventoryManager.AddToInventory(itemData, player);
    }

    public void Highlight()
    {
        
    }
}
