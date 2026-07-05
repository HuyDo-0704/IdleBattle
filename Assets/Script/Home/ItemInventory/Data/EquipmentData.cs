using UnityEngine;
public enum EquipmentType
{
    Weapon,
    Helmet,
    Armor,
    Gloves,
    Shoes,
    Ring,
    Necklace,
}
[CreateAssetMenu(menuName ="Idle RPG/Item/Equipment")]
public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;

    [Header("Base Stats")]
    public int hp;
    public int atk;
    public int def;
    public int speed;
    public int critRate;
    public int critDamage;
}
