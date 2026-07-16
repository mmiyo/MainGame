using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class InventoryRowScript : MonoBehaviour
{   
    private ItemType rowIdentifier;
     
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }   

    public void SetRowType(ItemType rowType)
    {
        rowIdentifier = rowType;
    
    }
}
