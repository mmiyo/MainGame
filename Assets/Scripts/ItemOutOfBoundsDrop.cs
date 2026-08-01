using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemOutOfBoundsDrop : MonoBehaviour, IDropHandler
{   
    private InventoryItemUI itemUI; // why is u a null reference stoopid
    

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

        Instantiate(itemUI.DraggedItem.Item);
        Destroy(itemUI.DraggedItem.gameObject);
             
    }
    
}
