using System;

[Serializable]
public class EquipmentItem : Item
{
    public EquipmentData Data;
    public int Level = 0;
    public Stats EquipmentStats = new();
    // Nhân vật đang trang bị
    public string equippedCharacterUID;

    public void InitializeStats(int Level)
    {
        EquipmentStats.hp = Data.GetHealthByLevel(Level);
        EquipmentStats.atk = Data.GetAttackByLevel(Level);
        EquipmentStats.def = Data.GetDefenseByLevel(Level);
        EquipmentStats.speed = Data.speed ;
        EquipmentStats.critRate = Data.critRate ;
        EquipmentStats.critDamage = Data.critDamage;
    }
    public EquipmentItem() : base()
    {

    }
}