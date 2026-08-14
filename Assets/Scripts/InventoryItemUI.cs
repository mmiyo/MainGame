using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler 
{   
    private Image iconRenderer;
    public ItemData data;
    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    [SerializeField] private GameObject item;
    private InventoryManager inventoryManager;
    private InventoryItemUI draggedItem;
    public Canvas inventoryCanvas; 
    public ItemSlotScript currentSlot;
    public ItemSlotScript previousSlot;
    public InventoryItemUI DraggedItem => draggedItem;
    public GameObject Item {get {return item;} set {item = value;}}
    public int itemCount;
    [SerializeField] private ItemScript itemScript;
    public ItemScript ItemScript => itemScript;
    public InventoryEntry inventoryEntry;
    [SerializeField] private TextMeshProUGUI itemCounter;
    [SerializeField] public UnityEvent updateCount;
 
    void Awake()
    {
        iconRenderer = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        item = itemScript.gameObject;

        //item = inventoryEntry.data.worldItem;
    }

    void Start()
    {
        itemCounter.SetText(inventoryEntry.itemCount.ToString());

    }

    void Update()
    {

    }

    public void UpdateCounter()
    {
        itemCounter.SetText(inventoryEntry.itemCount.ToString());
    }

    // Update is called once per frame 

    public void Initialize(InventoryEntry entry)
    {   
        data = entry.data;
        iconRenderer.sprite = entry.data.itemIcon;
        
        updateCount.Invoke();
        

        //Debug.Log("UI entry hashcode " + inventoryEntry.GetHashCode());

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
 
        gameObject.transform.SetParent(inventoryCanvas.transform, true);
        currentSlot.GetComponent<ItemSlotScript>().SetItem(null); 

        canvasGroup.blocksRaycasts = false;

        // Debug.Log("Dragged item is : " + draggedItem);
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        draggedItem = null;

        gameObject.transform.SetParent(currentSlot.transform, true);
        rectTransform.anchoredPosition = currentSlot.GetComponent<RectTransform>().anchoredPosition;
        rectTransform.anchoredPosition = Vector2.zero;

        canvasGroup.blocksRaycasts = true;

        // Debug.Log("gone " + draggedItem);
        //play sound
    }

    public void SewerSlide(InventoryEntry throwtspls)
    {   
        inventoryManager.ThrowAway(throwtspls); 
        Destroy(this.gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
         
    }
    
    

  

   
}
