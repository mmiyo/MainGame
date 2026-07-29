using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{   
    private Image iconRenderer;
    public ItemData data;
    private GameObject slot;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas inventoryCanvas;
    public InventoryItemUI inventoryItemUIInstance;    
    private InventoryManager inventoryManager;
    public ItemSlotScript currentSlot;
    void Awake()
    {
        iconRenderer = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame 

    public void Initialize(ItemData itemData)
    {   
        data = itemData;
        iconRenderer.sprite = itemData.itemIcon;
 
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
     }

    public void OnBeginDrag(PointerEventData eventData)
    {
 
    }

    public void OnDrag(PointerEventData eventData)
    {   
        slot.GetComponent<ItemSlotScript>().SetItem(null);// this stupid dookie head doesnt know what its reference is
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.SetParent(inventoryCanvas.transform, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
         
    }
    
    

  

   
}
