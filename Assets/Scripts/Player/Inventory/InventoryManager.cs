using System;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class InventoryManager : MonoBehaviour
{   
    private bool isOpen = false;
    private GameObject inventoryChild;
    private InventoryRowScript row;
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject inventoryRow;
    [SerializeField] private GameObject itemSlot; 

    private void Awake()
    {
        inventoryChild = transform.GetChild(0).gameObject;
         
    }

    private void Start()
    {
        foreach(ItemType i in Enum.GetValues(typeof(ItemType)))
        {
            GenerateRows(i);  
            GenerateSlot(i, 3, row.transform);
        }
    }

    public void OpenInventory(InputAction.CallbackContext context)
    {   
        if (!context.performed)
        return;

        isOpen = !isOpen;
        //Debug.Log(isOpen);
        inventoryChild.SetActive(isOpen);
 
    }

    private void GenerateRows(ItemType rowType)
    {   
        Debug.Log("Generated a " + rowType + " row");
        row = Instantiate(inventoryRow).GetComponent<InventoryRowScript>();
        row.transform.SetParent(inventoryContainer.transform, false);
       
    }
    
    private void GenerateSlot(ItemType slotType, int slotCount, Transform inventoryRow)
    {   
        for(int i = 0; i < slotCount; i++)
        {
            ItemSlotScript slot = Instantiate(itemSlot, inventoryRow).GetComponent<ItemSlotScript>();
            Debug.Log("Generated a  " + slotType + " slot");
            slot.SetAllowedType(slotType);
        }
    }

    public void AddToInventory(ItemData itemData, PlayerManager player)
    {
        Debug.Log("added item " + itemData.itemName + " to " + player.name + " inv." + " This is a " + itemData.itemType);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}

    