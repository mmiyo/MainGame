using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{   
    private ItemType allowedType;    
    private InventoryItemUI itemUI = null;
    public InventoryItemUI ItemUI {get {return itemUI;} set {itemUI = value;}}
    public ItemType ItemType{ get {return allowedType;}}
    private ItemSlotScript previousSlot;
    private ItemSlotScript currentSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        currentSlot = this;
        previousSlot = currentSlot;
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

    public InventoryItemUI CarriedItem(InventoryItemUI item)
    {   
        if(item != null)
        {
            itemUI = item;
            return itemUI;
        }
        return null;             
    }

    public void NotifySlot()
    {   
    
        Debug.Log(gameObject.name + itemUI);
        currentSlot.ItemUI = itemUI;
        previousSlot.ItemUI = null;   
           
        Debug.Log("Current " + currentSlot.name);
        Debug.Log("Previous " + previousSlot.name);
        Debug.Log(gameObject.name + itemUI);
    
    }

    public void OnDrop(PointerEventData eventData)
    {   
        Debug.Log(allowedType);
/*
        ItemSlotScript previousSlot = eventData.pointerDrag.GetComponent<InventoryItemUI>().PreviousSlot;
        ItemSlotScript currentSlot = eventData.pointerDrag.GetComponent<InventoryItemUI>().CurrentSlot;
 */

        if(itemUI == null && eventData.pointerDrag.GetComponent<InventoryItemUI>().data.itemType == allowedType)
        {   
 
        
            //current slot will be this
            //make itemui null on previous slot
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.SetAsLastSibling();
            itemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();

            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            itemRect.anchoredPosition = Vector2.zero;
                                                                                   
            NotifySlot();

             //Debug.Log(itemUI.GetComponent<RectTransform>().anchoredPosition);
            //Debug.Log(ItemUI.transform.localPosition);
        }
        else
        {
            
        }
       
        
        /*{
            ItemUI.transform.SetParent(gameObject.transform, false);
        }*/
    }
  

     
    


}
