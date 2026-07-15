using System;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class InventoryManager : MonoBehaviour
{   
    private PlayerInput iComp_playerInput;
    private bool isOpen = false;
    private GameObject inventoryChild;
    private ItemData itemData;
    [SerializeField] private GameObject inventoryRows;
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private GameObject weaponRow;
 
    private void Awake()
    {
        iComp_playerInput = GetComponent<PlayerInput>();
        inventoryChild = transform.GetChild(0).gameObject;
         
    }

    private void Start()
    {
        GenerateSlot(ItemType.Weapon, 3, weaponRow.transform);


    }

    public void OpenInventory(InputAction.CallbackContext context)
    {   
        if (!context.performed)
        return;

        isOpen = !isOpen;
        Debug.Log(isOpen);
        inventoryChild.SetActive(isOpen);
 
    }

    
    
    // Update is called once per frame
    void Update()
    {
        foreach(ItemType i in Enum.GetValues(typeof(ItemType)))
        {
            GenerateRows(i, inventoryChild.transform);  
        }
        
    }

    //TO DO: Change generation of rows, create a prefab for rows, then reference it here. 
    //This row prefab will contain a Row script and a horizontal layout group. And a sprite renderer obviously XDD
    // After that, create GenerateRow(ItemType rowType, Transform pos). Pos would be the inventory container 
    // Wherein it has the component vertical layout group that lets the rows fall in to place accordingly
    // Once the rows are generated, use its transform as GenerateSlot()'s inventoryRow
    private void GenerateRows(ItemType rowType, Transform pos)
    {
        //instantiate the rows
        Debug.Log(rowType);
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
}

  