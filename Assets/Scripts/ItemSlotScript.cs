using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{   
    private ItemType allowedType;    
    private InventoryItemUI itemUI; 
    public InventoryItemUI ItemUI {get {return itemUI;} set {itemUI = value;}}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }
    

    // Update is called once per frame
    void Update()
    {
         
    }

    public void SetAllowedType(ItemType allowedSlot)
    {   
        allowedType = allowedSlot;
        //Debug.Log("Allowed Item is only the " + allowedSlot + " Type");
    }

    public InventoryItemUI CarriedItem(InventoryItemUI item)
    {   
        if(item != null)
        {
            itemUI = item;
            return itemUI;
        }
        return null;             
 
    }
  

     
    


}
