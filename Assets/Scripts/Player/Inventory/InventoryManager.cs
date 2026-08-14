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
    [SerializeField] private PlayerManager player;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject inventoryRow;
    [SerializeField] private GameObject itemSlotPrefab; 
    [SerializeField] private GameObject itemUI;
    [SerializeField] public List<InventoryEntry> inventoryData = new();
    private List<ItemSlotScript> generatedSlots = new ();
    Dictionary<ItemType, int> rowLimit = new();
    private bool isOpen = false;
    private GameObject inventoryChild;
    private InventoryRowScript row;
    private InventoryItemUI ui;
    private ItemSlotScript slot;
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
    
    GameObject sceneObject;
    ItemScript objectScript;
    public void ThrowAway(InventoryEntry itemToThrow)
    {   
        sceneObject = Instantiate(ui.Item, player.transform);
        sceneObject.transform.SetParent(null);
        objectScript = sceneObject.GetComponent<ItemScript>();

        objectScript.inventoryEntry = itemToThrow;

        objectScript.name = objectScript.inventoryEntry.data.itemName;
        //Debug.Log(objectScript.inventoryEntry.data.itemName);

        objectScript.inventoryEntry.data = itemToThrow.data;
        Debug.Log("this item's item count is " + " " + objectScript.inventoryEntry.itemCount);
         
        inventoryData.Remove(itemToThrow);

        //DO NOT REMOVE THESE THEY CAN STILL BE REUSED FOR TRACKING ITEMS
        /*
        Debug.Log("the item to be removed is the " + itemToRemove.data.itemName);
        Debug.Log("removing " + itemToRemove.data.itemName + " with hash code " + itemToRemove.GetHashCode());

        Debug.Log("The item to instantiate is " + ui.inventoryEntry.data.itemName);
        Debug.Log("the hash code of the item to instantiate is " + ui.inventoryEntry.GetHashCode());
        Debug.Log("Instantiating " + ui.Item.GetComponent<ItemScript>().inventoryEntry.data.itemName);
        Debug.Log("The hash code of the newly instantiated item is " + ui.Item.GetComponent<ItemScript>().inventoryEntry.GetHashCode());
        */  
    }

    public void OpenInventory(InputAction.CallbackContext context)
    {   
        if (!context.performed)
        {   
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
        //Debug.Log("inventory manager addtoinv function " + item.GetHashCode());  
        ItemSlotScript compatibleSlot = generatedSlots.Find(s => s.ItemUI == null && s.AllowedItemType == item.data.itemType);
        compatibleSlot.CreateOnSlot(item, compatibleSlot);
        compatibleSlot.SetItem(ui);
        ui.updateCount.Invoke();
         
    }

    public void CreateItem(InventoryEntry entry, ItemSlotScript slot)
    {       
        inventoryData.Add(entry);    
        Instantiate(entry, slot);
 
    }
    
    private void Instantiate(InventoryEntry entry, ItemSlotScript emptySlot)
    {   
        ui = Instantiate(itemUI).GetComponent<InventoryItemUI>();
        ui.inventoryCanvas = mainCanvas;
        ui.inventoryEntry = entry;
 
        ui.transform.SetParent(emptySlot.transform, false);
        ui.name = entry.data.itemName;
        ui.Initialize(entry);
        
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log("surplus" + " " + itemSurplus);
        /*
        foreach(InventoryEntry item in inventoryData)
        {
            Debug.Log("Inventory items :" + item.data.itemName + " " + item.GetHashCode());
        }*/
        
    }
}

    