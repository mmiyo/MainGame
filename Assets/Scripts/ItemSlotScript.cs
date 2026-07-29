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
        if(itemUI)
        Debug.Log(allowedType + gameObject.name + " " + itemUI);
 
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
            Debug.Log(itemUI.currentSlot);
            return itemUI;
        }
        return itemUI;       
    }
 
    public void OnDrop(PointerEventData eventData)
    {   
        Debug.Log(allowedType);
 
        if(ItemUI == null && eventData.pointerDrag.GetComponent<InventoryItemUI>().data.itemType == allowedType)
        {
            SetItem(eventData.pointerDrag.GetComponent<InventoryItemUI>());
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.SetAsLastSibling();

            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            itemRect.anchoredPosition = Vector2.zero;
        }
        
        /*
        if(itemUI == null && eventData.pointerDrag.GetComponent<InventoryItemUI>().data.itemType == allowedType)
        {   
            //current slot will be this
            //make itemui null on previous slot
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.SetAsLastSibling();
            itemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();

            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            itemRect.anchoredPosition = Vector2.zero;
                                                                                   
        

             //Debug.Log(itemUI.GetComponent<RectTransform>().anchoredPosition);
            //Debug.Log(ItemUI.transform.localPosition);
        }
        else
        {
            
        }
       
        
        {
            ItemUI.transform.SetParent(gameObject.transform, false);
        }*/
    }
  

     
    


}
