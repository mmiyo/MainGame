using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemOutOfBoundsDrop : MonoBehaviour, IDropHandler
{   
    [SerializeField] private InventoryManager inventoryManager;
    private InventoryItemUI itemUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
     }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {   
        itemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();
 
        inventoryManager.RemoveItem(itemUI.data);
        Instantiate(itemUI.DraggedItem.Item);
        Destroy(itemUI.DraggedItem.gameObject);
  
    }
    
}
