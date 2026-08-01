using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon")]
public class WeaponData : ItemData
{
    public float attackSpeed;
    public float damage;
    public WeaponType weaponType;
}

public enum WeaponType {Sword, Spear, Axe, Mace, Bow, Staff}