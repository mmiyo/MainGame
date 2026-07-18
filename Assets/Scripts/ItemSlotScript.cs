using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour//, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{   
    private ItemType allowedType;
    private bool occupied = false;

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

    public void DisplayItem(Image itemSprite, GameObject  item)
    {
        
    }
}
