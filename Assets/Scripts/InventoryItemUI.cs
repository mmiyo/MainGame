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
    public ItemSlotScript previousSlot;

    
    void Awake()
    {
        iconRenderer = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
       
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Update is called once per frame 

    public void Initialize(ItemData itemData)
    {   
        data = itemData;
        iconRenderer.sprite = itemData.itemIcon;
 
    }

     public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
    }

    public void OnDrag(PointerEventData eventData)
    {   
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor; 
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.SetParent(inventoryCanvas.transform, true);
        currentSlot.GetComponent<ItemSlotScript>().SetItem(null); 
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
         
    }
    
    

  

   
}
