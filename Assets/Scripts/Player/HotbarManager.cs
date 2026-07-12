using System.Runtime.CompilerServices;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{   
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private GameObject hotbarSlots;
    private int hotbarLimit = 3;
    public int HotbarLimit {get {return hotbarLimit;}}
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < hotbarLimit; i++)
        {
            Instantiate(itemSlot, hotbarSlots.transform);
        }
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
