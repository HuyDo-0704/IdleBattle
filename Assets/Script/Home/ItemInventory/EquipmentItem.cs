using System;

[Serializable]
public class EquipmentItem : Item
{
    // Loại trang bị
    public EquipmentType equipmentType;

    // Chỉ số cộng thêm ngẫu nhiên
    public int attack;
    public int defense;
    public int hp;

    // Chỉ số phụ (Random Stat)
    public int critRate;
    public int critDamage;
    public int attackSpeed;

    // Độ bền (nếu sau này cần)
    public int durability;

    // Nhân vật đang trang bị
    public string equippedCharacterUID;

    public EquipmentItem() : base()
    {

    }
}