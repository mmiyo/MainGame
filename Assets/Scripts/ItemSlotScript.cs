using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{   
    private RectTransform rectTransform;
     private ItemType allowedType;    
    private InventoryItemUI itemUI; 
    public ItemSlotScript itemSlotScriptInstance;
    public InventoryItemUI ItemUI {get {return itemUI;} set {itemUI = value;}}
    public ItemType ItemType{ get {return allowedType;}}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {   
        rectTransform = GetComponent<RectTransform>();
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

    //if an inventory item gets dragged to a diff slot put his goofy ahh there
    public void OnDrop(PointerEventData eventData)
    {   
        if(itemUI == null)
        {       
            Debug.Log(gameObject.name + ": i am empty");
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.SetAsLastSibling();
            itemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();

            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            itemRect.anchoredPosition = Vector2.zero;
            Debug.Log(gameObject.name + " " + itemUI + " is now my child");

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
