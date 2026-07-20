using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
 
public class InventoryManager : MonoBehaviour
{   
    private bool isOpen = false;
    private GameObject inventoryChild;
    private InventoryRowScript row;
    private InventoryItemUI ui;
    private ItemSlotScript slot;
    private List<ItemData> inventoryData = new();
    private List<ItemSlotScript> generatedSlots = new ();
    Dictionary<ItemType, int> rowLimit = new();
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject inventoryRow;
    [SerializeField] private GameObject itemSlotPrefab; 
    [SerializeField] private GameObject itemUI;

    private void Awake()
    {
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
        row = Instantiate(inventoryRow).GetComponent<InventoryRowScript>();
        row.transform.SetParent(inventoryContainer.transform, false);
        row.SetRowType(rowType);
        
        /*Debug.Log("Generated a " + rowType + " row");
        Debug.Log("Generated " + rowType + " slots");*/
        GenerateSlot(rowType, rowLimit[rowType], row.transform);

    }
    
    private void GenerateSlot(ItemType slotType, int slotCount, Transform inventoryRow)
    {   
        for(int i = 0; i < slotCount; i++)
        {
            slot = Instantiate(itemSlotPrefab, inventoryRow).GetComponent<ItemSlotScript>();
            slot.SetAllowedType(slotType);
            generatedSlots.Add(slot);
            //Debug.Log("i have " + generatedSlots.Count + " amount of slost");
        }
    }

    //add a for loop that runs through generatedSlots[0] and check if their 
    //ItemSlotScript's InventoryItemUI is null, if null, 
    //AddToInventory will instantiate itemUI on that slot.

    public void AddToInventory(ItemData itemData, PlayerManager player)
    {   
        inventoryData.Add(itemData);
        foreach(ItemSlotScript slot in generatedSlots)
        {   
            if(slot.CarriedItem() == null)
            {
                Instantiate(itemData, slot); 
                break;  
                //emptySlot.CarriedItem(); should be equal to the item that is to be added 
                
            }
        }
    }
    
    private void Instantiate(ItemData data, ItemSlotScript slot)
    {
        ui = Instantiate(itemUI).GetComponent<InventoryItemUI>();
        ui.transform.SetParent(slot.transform, false);
        ui.Initialize(data);
    }

    // Update is called once per frame
    void Update()
    {
         
        
    }
}

    