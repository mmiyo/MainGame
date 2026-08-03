using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
 
public class InventoryManager : MonoBehaviour
{   
    private bool isOpen = false;
    private GameObject inventoryChild;
    private InventoryRowScript row;
    private InventoryItemUI ui;
    private ItemSlotScript slot;
    [SerializeField] public List<InventoryEntry> inventoryData = new();
    private List<ItemSlotScript> generatedSlots = new ();
    Dictionary<ItemType, int> rowLimit = new();
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject inventoryRow;
    [SerializeField] private GameObject itemSlotPrefab; 
    [SerializeField] private GameObject itemUI;
    public ItemSlotScript Slot => slot;
    public InventoryManager invInstance;
    private void Awake()
    {   
        itemSlotPrefab.GetComponent<ItemSlotScript>().inventoryManager = this;

        inventoryChild = transform.GetChild(0).gameObject;
        //inv rows
        rowLimit.Add(ItemType.Weapon, 3);
        rowLimit.Add(ItemType.Curio, 5);
        rowLimit.Add(ItemType.Consumable, 4);
        rowLimit.Add(ItemType.Throwable, 4);
        rowLimit.Add(ItemType.Skill, 3);          

    }

    private void Start()
    {
        foreach(ItemType i in Enum.GetValues(typeof(ItemType)))
        {  
            GenerateRows(i);  
        }
    }

    public void RemoveItem(ItemScript itemToRemove)
    {
        Debug.Log("item to be removed is " + itemToRemove.itemData.itemName);
        //CHANGE THIS: its supposed to remove the itemdata linked to the dropped itemui
        //maybe even change the logic on how item is added to inventorydata so that the added data
        //inside the list is linked to the ui object
        //OR change this to a list of itemui instead of itemdata lmaolol
    }

    public void OpenInventory(InputAction.CallbackContext context)
    {   
        if (!context.performed)
        {   
            ui.gameObject.SetActive(true);
            return;
        }

        isOpen = !isOpen;
        inventoryChild.SetActive(isOpen);
        if(inventoryChild.activeSelf)
        {
            ui.gameObject.SetActive(true);
            ui.canvasGroup.blocksRaycasts = true;

        }
    }

    private void GenerateRows(ItemType rowType)
    {   
        row = Instantiate(inventoryRow).GetComponent<InventoryRowScript>();
        row.transform.SetParent(inventoryContainer.transform, false);
        row.SetRowType(rowType);
        
        GenerateSlot(rowType, rowLimit[rowType], row.transform);

    }
    
    private void GenerateSlot(ItemType slotType, int slotCount, Transform inventoryRow)
    {   
        int slotCounter = 1;
        for(int i = 0; i < slotCount; i++)
        {   
            slot = Instantiate(itemSlotPrefab, inventoryRow).GetComponent<ItemSlotScript>();
            slot.name = "Slot: " + slotCounter;
            slotCounter++;
            slot.AllowedType(slotType);
            generatedSlots.Add(slot);

        }
    }

    public void AddToInventory(InventoryEntry item)
    {   
        foreach(ItemSlotScript slot in generatedSlots)
        {   
            if(slot.ItemUI == null && slot.ItemType == item.data.itemType)
            {   
                inventoryData.Add(item);
                Instantiate(item.data, slot);
                slot.SetItem(ui);
                break;
            }
            if(slot.ItemUI != null && slot.ItemUI.data.isStackable && slot.ItemUI.data.ID == item.data.ID)
            {
                Debug.Log("u got a dupe");
                
                break;
            }
            else
            {   
                continue;
            }
        }
    }
    
    private void Instantiate(ItemData data, ItemSlotScript emptySlot)
    {   
        ui = Instantiate(itemUI).GetComponent<InventoryItemUI>();
        ui.inventoryCanvas = mainCanvas;
        ui.transform.SetParent(emptySlot.transform, false);
        ui.name = data.itemName;
        ui.Initialize(data);
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(InventoryEntry item in inventoryData)
        {
            Debug.Log(item.data.itemName);
        }
    }
}

    