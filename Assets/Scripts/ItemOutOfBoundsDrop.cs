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
      //  Debug.Log(itemUI.ItemScript.inventoryEntry.data.itemName);  

        inventoryManager.RemoveItem(itemUI.inventoryEntry); // get a ref to pass here so u can remove that from InventoryManager
        Instantiate(itemUI.DraggedItem.Item);
        Destroy(itemUI.DraggedItem.gameObject);
  
    }
    
}
