using UnityEngine;

[CreateAssetMenu(menuName ="Idle RPG/Item/Consumable")]
public class ConsumableData : ItemData
{
    //public ConsumableType consumableType;

    public bool stackable;

    public int maxStack;

    public int exp;

    public int gold;
}
