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

    public void RemoveItem(InventoryEntry itemToRemove)
    {   
        
        
        ui.Item.GetComponent<ItemScript>().inventoryEntry = itemToRemove;
        ui.Item.name = ui.Item.GetComponent<ItemScript>().inventoryEntry.data.itemName;
        Instantiate(ui.Item);

        Debug.Log("the item to be removed is the " + itemToRemove.data.itemName);
        Debug.Log("removing " + itemToRemove.data.itemName + " with hash code " + itemToRemove.GetHashCode());

        Debug.Log("The item to instantiate is " + ui.inventoryEntry.data.itemName);
        Debug.Log("the hash code of the item to instantiate is " + ui.inventoryEntry.GetHashCode());
        Debug.Log("Instantiating " + ui.Item.GetComponent<ItemScript>().inventoryEntry.data.itemName);
        Debug.Log("The hash code of the newly instantiated item is " + ui.Item.GetComponent<ItemScript>().inventoryEntry.GetHashCode());

        inventoryData.Remove(itemToRemove);



        //Instantiate(itemToRemove.data.itemPrefab);
 
        //Instantiate(ui.Item);
        //Instantiate an item script using itemOutOfBound's pointereventdata 
        //and use itemToREmove's entry for its stats

        //add logic here latuhhh
        //OutOfBoundsDrop will instantiate
        //that same entry again so when u pick it up its data is the same yeayeayeayeayea
        
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
        Debug.Log("inventory manager addtoinv function " + item.GetHashCode());  
        foreach(ItemSlotScript slot in generatedSlots)
        {   
            if(slot.ItemUI != null && slot.ItemUI.data.isStackable && slot.ItemUI.data.ID == item.data.ID)
            {
                Debug.Log("u got a dupe");
                
                break;
            }
            if(slot.ItemUI == null && slot.ItemType == item.data.itemType)
            {   
                inventoryData.Add(item);
                Instantiate(item, slot);
                slot.SetItem(ui);
                break;
            }
             
            else
            {   
                continue;
            }
        }
    }
    
    private void Instantiate(InventoryEntry entry, ItemSlotScript emptySlot)
    {   
        ui = Instantiate(itemUI).GetComponent<InventoryItemUI>();
        ui.inventoryCanvas = mainCanvas;
        ui.Item.GetComponent<ItemScript>().itemData = entry.data;
        ui.inventoryEntry = entry;
        ui.transform.SetParent(emptySlot.transform, false);
        ui.name = entry.data.itemName;
        ui.Initialize(entry);
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(InventoryEntry item in inventoryData)
        {
            Debug.Log("Inventory items :" + item.data.itemName + " " + item.GetHashCode());
        }
    }
}

    