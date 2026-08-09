using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{   
    public InventoryManager inventoryManager;
    private ItemType allowedType;    
    private InventoryItemUI itemUI = null;
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
        // Debug.Log(gameObject.name + itemUI);
    }

    public ItemType AllowedType(ItemType allowedSlot)
    {   
        allowedType = allowedSlot;
        return allowedType;
    }

    public InventoryItemUI SetItem(InventoryItemUI item)
    {                
        if(item != null)
        {   
            itemUI = item;  
            itemUI.currentSlot = this;
            return itemUI;
        }
        else
        {
            itemUI = null;
        }
        return itemUI;       
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
        DropType dropType = DropStateManager(eventData);
        
        switch(dropType)
        {
            case DropType.Drop:
            DropEmpty(eventData);
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

        if(ItemUI == null && itemDragged.data.itemType == allowedType)
        {   
            return DropType.Drop;
        }
        if(ItemUI != null && itemDragged.data.isStackable && itemDragged.data.ID == ItemUI.inventoryEntry.data.ID)
        {
            return DropType.Merge;
        }
        if(ItemUI != null)
        {
            return DropType.Swap;
        }

        return DropType.Idle;
        
    }

    private void DropEmpty(PointerEventData dropData)
    {
        SetItem(dropData.pointerDrag.GetComponent<InventoryItemUI>());
        dropData.pointerDrag.transform.SetParent(transform, false);
        dropData.pointerDrag.transform.SetAsLastSibling();

        RectTransform itemRect = dropData.pointerDrag.GetComponent<RectTransform>();
        itemRect.anchoredPosition = Vector2.zero;
    }

    private void DropMerge(PointerEventData dropData)
    {   
        Debug.Log("same shi");
        ItemUI.currentSlot = dropData.pointerDrag.GetComponent<InventoryItemUI>().currentSlot;
    }

    private void DropSwap(PointerEventData dropData)
    {
        
    }
}
