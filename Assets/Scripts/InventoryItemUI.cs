using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{   
    private Image iconRenderer;
    private RectTransform rectTransform;
    public Canvas inventoryCanvas;
    void Awake()
    {
        iconRenderer = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame 

    public void Initialize(ItemData itemData)
    {   
        iconRenderer.sprite = itemData.itemIcon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        Debug.Log("clicked");
     }

    //unparent it from the slot so it can be parented to the slot it drops on. 
    //if it doesnt drop on a slot the item is thrown away
    public void OnDrag(PointerEventData eventData)
    {   
        Debug.Log("dragged");
        Debug.Log(inventoryCanvas);
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("held");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("let go");
    }

}
