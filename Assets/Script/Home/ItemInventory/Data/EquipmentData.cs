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
    [Header("Level Scaling")]
    public float healthPerLevel = 10;
    public float manaPerLevel = 5;
    public float attackPerLevel = 2;
    public float defensePerLevel = 1;
    private int baseLevel = 1;

    // --------- Optional: Function to get stats by level ----------
    public float GetHealthByLevel(float level)
    {
        return hp + (level - baseLevel) * healthPerLevel;
    }


    public float GetAttackByLevel(float level)
    {
        return atk + (level - baseLevel) * attackPerLevel;
    }

    public float GetDefenseByLevel(float level)
    {
        return def + (level - baseLevel) * defensePerLevel;
    }
}