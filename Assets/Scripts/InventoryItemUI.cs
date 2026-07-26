using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{   
    private Image iconRenderer;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas inventoryCanvas;
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
        iconRenderer.sprite = itemData.itemIcon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        Debug.Log("clicked");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("held");
    }

    public void OnDrag(PointerEventData eventData)
    {   
        Debug.Log("dragged");
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
        canvasGroup.blocksRaycasts = false;
        gameObject.transform.SetParent(inventoryCanvas.transform, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("let go");
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("boop");
    }
    
    

  

   
}
