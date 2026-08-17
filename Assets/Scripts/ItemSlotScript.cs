using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{   
    public InventoryManager inventoryManager;
    private ItemType allowedType;   
    public ItemType AllowedItemType => allowedType; 
    [SerializeField] private InventoryItemUI itemUI = null;
    private int count;
    public InventoryItemUI ItemUI {get {return itemUI;} set {itemUI = value;}}
    public ItemType ItemType{ get {return allowedType;}}

    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    public ItemType AllowedType(ItemType allowedSlot)
    {   
        allowedType = allowedSlot;
        return allowedType;
    }

    public InventoryItemUI SetItem(InventoryItemUI item)
    {   
        itemUI = item;
        if(item != null)
        {   
            itemUI.currentSlot = this;
        }
      
        return itemUI;       
    }

    public void CreateOnSlot(InventoryEntry item, ItemSlotScript slot)
    {   
        if(ItemUI != null )
        {
            AddExisting(item, slot);
        }
        else 
        {   
            inventoryManager.CreateItem(item, slot);             
        }
    }
    
    private void AddExisting(InventoryEntry itemToGetCount, ItemSlotScript newSlot)
    {   
        newSlot = inventoryManager.generatedSlots.Find(n => n.allowedType == itemToGetCount.data.itemType && n.ItemUI == null);      

        int incomingItemCount = itemToGetCount.itemCount;
        int slotItemCount = ItemUI.inventoryEntry.itemCount;  
        int totalCount = slotItemCount + incomingItemCount;
        int itemSurplus = totalCount - ItemUI.inventoryEntry.data.maxStack;  
        
        ItemUI.inventoryEntry.itemCount += incomingItemCount;

        if(ItemUI.inventoryEntry.itemCount > ItemUI.inventoryEntry.data.maxStack )
        {   
            ItemUI.inventoryEntry.itemCount -= itemSurplus;
            itemToGetCount.itemCount = itemSurplus;
            ItemUI.updateCount.Invoke();
            inventoryManager.CreateItem(itemToGetCount, newSlot);  
        }
        
        ItemUI.updateCount.Invoke();

         
        //Debug.Log("incoming item count: " + incomingItemCount);
        //Debug.Log("existing item count: " + ItemUI.inventoryEntry.itemCount);
        
       
    }

    private enum DropType //state machine of doom and despair miyo edition™
    {   
        Idle,
        Drop,
        Merge,
        Swap,

    }    

    public void OnDrop(PointerEventData eventData)
    {   
        /*
        Debug.Log("DROPPEd ON: " + gameObject.name + " WITH INSTANCE ID " + GetInstanceID());
        Debug.Log("ITEM UI: " + ItemUI);*/
        DropType dropType = DropStateManager(eventData);
        
        switch(dropType)
        {
            case DropType.Drop:
            DropEmpty(eventData);
            //Debug.Log(gameObject.name + " " + ItemUI);
            break;

            case DropType.Merge:
            DropMerge(eventData);
            break;

            case DropType.Swap:
            DropSwap(eventData);
            break;
            
        }
    }

    private DropType DropStateManager(PointerEventData cursorData)
    {   
        InventoryEntry itemDragged = cursorData.pointerDrag.GetComponent<InventoryItemUI>().inventoryEntry;
        if(itemUI == null && itemDragged.data.itemType == allowedType)
        {   
            return DropType.Drop;
        }
        if(itemUI != null )
        {   
            Debug.Log("ITEM UI EXISTS");

            return DropType.Merge;
        }
        if(itemUI != null)
        {
            return DropType.Swap;
        }

        return DropType.Idle;
        
    }

    private void DropEmpty(PointerEventData dropData)
    {   
        //Debug.Log("dropped on " + ItemUI);
        SetItem(dropData.pointerDrag.GetComponent<InventoryItemUI>());
        dropData.pointerDrag.transform.SetParent(transform, false);
        dropData.pointerDrag.transform.SetAsLastSibling();

        RectTransform itemRect = dropData.pointerDrag.GetComponent<RectTransform>();
        itemRect.anchoredPosition = Vector2.zero;
    }

    private void DropMerge(PointerEventData dropData)
    {   
        Debug.Log("same shi");

    }

    private void DropSwap(PointerEventData dropData)
    {
        
    }
}
