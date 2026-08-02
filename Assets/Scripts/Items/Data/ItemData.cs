using UnityEngine;
using UnityEngine.UI;

public abstract class ItemData : ScriptableObject
{   
    public string itemName;
    public Sprite itemIcon;
    public ElementType elementType;
    public Rarity rarity;
    public ItemType itemType;
    public GameObject itemPrefab;
    public bool isStackable;
    public bool isUpgradeable;
}

    public enum Rarity { Common, Rare, Epic, Legendary, Mythical };
    public enum ElementType { None, Ignis, Nihil, Celestial, Toxic, Frost, Aqua } 
    public enum ItemType { Weapon, Curio, Consumable, Throwable, Skill }
    
