using UnityEngine;

public abstract class ItemData : ScriptableObject
{   
    public string itemName;
    public Sprite itemIcon;
    public ElementType elementType;
    public Rarity rarity;
}

    public enum Rarity {Common, Rare, Epic, Legendary, Mythical};
    public enum ElementType {None, Ignis, Nihil, Celestial, Toxic, Frost, Aqua, } 
    
