using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{   
    private Image iconRenderer;

    void Awake()
    {
        iconRenderer = GetComponent<Image>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(ItemData itemData)
    {   
        iconRenderer.sprite = itemData.itemIcon;
    }

    
}
