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
    }

    private void GenerateSlot(ItemType slotType, int slotCount, Transform inventoryRow)
    {   
        for(int i = 0; i < slotCount; i++)
        {
            Instantiate(itemSlot, inventoryRow);
            Debug.Log("Generated a  " + slotType + " slot");
        }
    }

    public void AddToInventory(ItemData itemData, PlayerManager player)
    {
        Debug.Log("added item " + itemData.itemName + " to " + player.name + " inv." + " This is a " + itemData.itemType);
    }
}

  