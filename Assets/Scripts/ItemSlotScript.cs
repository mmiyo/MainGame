using UnityEngine;

public class ItemSlotScript : MonoBehaviour
{   
    private ItemType allowedType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAllowedType(ItemType allowedSlot)
    {   
        allowedType = allowedSlot;
        Debug.Log("Allowed Item is only the " + allowedSlot + " Type");
    }
}
