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
    private List<ItemData> inventoryData = new();
    Dictionary<ItemType, int> rowLimit = new();
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject inventoryRow;
    [SerializeField] private GameObject itemSlot; 
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
            ItemSlotScript slot = Instantiate(itemSlot, inventoryRow).GetComponent<ItemSlotScript>();
            slot.SetAllowedType(slotType);
        }
    }

    //TO DO: create a list for every generated slot prefab to store references 
    //so that ui will be parented to those slots specifically
    //and for AddToInventory() to dynamically add and modify 
    //itemData whichever slot you drag and drop itemData's sprite in

    public void AddToInventory(ItemData itemData, PlayerManager player)
    {   
        inventoryData.Add(itemData);
        ui = Instantiate(itemUI, itemSlot.transform).GetComponent<InventoryItemUI>();
        ui.Initialize(itemData);
    
    }

    // Update is called once per frame
    void Update()
    {
         
        
    }
}

    