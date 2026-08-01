using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{   
    private Image iconRenderer;
    public ItemData data;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private GameObject item;
    private InventoryManager inventoryManager;
    private InventoryItemUI draggedItem;
    public Canvas inventoryCanvas; 
    public ItemSlotScript currentSlot;
    public ItemSlotScript previousSlot;
    public InventoryItemUI DraggedItem => draggedItem;
    public GameObject Item => item;
 
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
        item.GetComponent<PickUpScript>().itemData = itemData;
        iconRenderer.sprite = itemData.itemIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //play sound
    }

    public void OnDrag(PointerEventData eventData)
    {   
        draggedItem = eventData.pointerDrag.GetComponent<InventoryItemUI>();

        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor; 
        canvasGroup.blocksRaycasts = false;

        gameObject.transform.SetParent(inventoryCanvas.transform, true);
        currentSlot.GetComponent<ItemSlotScript>().SetItem(null); 

        Debug.Log("Dragged item is : " + draggedItem);
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        draggedItem = null;

        gameObject.transform.SetParent(currentSlot.transform, true);
        rectTransform.anchoredPosition = currentSlot.GetComponent<RectTransform>().anchoredPosition;
        rectTransform.anchoredPosition = Vector2.zero;

        canvasGroup.blocksRaycasts = true;

        Debug.Log("gone " + draggedItem);
        //play sound
    }

    public void OnDrop(PointerEventData eventData)
    {
         
    }
    
    

  

   
}
