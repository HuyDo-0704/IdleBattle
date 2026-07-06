using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;
    public EquipmentPanelUI UI;
    private void Awake()
    {
        Instance = this;
    }

    public bool Equip(Character character, EquipmentItem item)
    {
        if (character == null || item == null)
            return false;

        // nếu item đang mặc bởi nhân vật khác
        if (!string.IsNullOrEmpty(item.equippedCharacterUID))
        {
            Debug.Log("Item already equipped.");
            return false;
        }

        // lấy đồ đang mặc ở slot đó
        EquipmentItem oldItem = GetEquipment(character, item.Data.equipmentType);

        // tháo nếu có
        if (oldItem != null)
        {
            oldItem.isEquipped = false;
            oldItem.equippedCharacterUID = "";
        }

        // mặc đồ mới
        SetEquipment(character, item);

        item.isEquipped = true;
        item.equippedCharacterUID = character.uid;
        // cập nhập chỉ số item
        item.InitializeStats(item.level);
        UpdateEquipmentStats(character);
        // cập nhập lại chỉ số toàn bộ 
        character.currentStats.RecalculateStats(character.CurrentLevel);
        // cập nhập hình ảnh 
        UI.Refresh();
        CharDetail.Instance.statsIndex.UpdateStats(character);
        return true;
    }

    public void Unequip(Character character, EquipmentType type)
    {
        EquipmentItem item = GetEquipment(character, type);

        if (item == null)
            return;

        item.isEquipped = false;
        item.equippedCharacterUID = "";

        RemoveEquipment(character, type);

        UpdateEquipmentStats(character);
    }

    public EquipmentItem GetEquipment(Character character, EquipmentType type)
    {
        if (character == null)
            return null;

        switch (type)
        {
            case EquipmentType.Weapon: return character.equipments.weapon;
            case EquipmentType.Helmet: return character.equipments.helmet;
            case EquipmentType.Armor: return character.equipments.armor;
            case EquipmentType.Gloves: return character.equipments.gloves;
            case EquipmentType.Shoes: return character.equipments.shoes;
            case EquipmentType.Ring: return character.equipments.ring;
            case EquipmentType.Necklace: return character.equipments.necklace;
        }

        return null;
    }

    private void SetEquipment(Character character, EquipmentItem item)
    {
        switch (item.Data.equipmentType)
        {
            case EquipmentType.Weapon:
                character.equipments.weapon = item;
                break;

            case EquipmentType.Helmet:
                character.equipments.helmet = item;
                break;

            case EquipmentType.Armor:
                character.equipments.armor = item;
                break;

            case EquipmentType.Gloves:
                character.equipments.gloves = item;
                break;

            case EquipmentType.Shoes:
                character.equipments.shoes = item;
                break;

            case EquipmentType.Ring:
                character.equipments.ring = item;
                break;

            case EquipmentType.Necklace:
                character.equipments.necklace = item;
                break;
        }
    }

    private void RemoveEquipment(Character character, EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Weapon:
                character.equipments.weapon = null;
                break;

            case EquipmentType.Helmet:
                character.equipments.helmet = null;
                break;

            case EquipmentType.Armor:
                character.equipments.armor = null;
                break;

            case EquipmentType.Gloves:
                character.equipments.gloves = null;
                break;

            case EquipmentType.Shoes:
                character.equipments.shoes = null;
                break;

            case EquipmentType.Ring:
                character.equipments.ring = null;
                break;

            case EquipmentType.Necklace:
                character.equipments.necklace = null;
                break;
        }
    }

    public void UpdateEquipmentStats(Character character)
    {
        character.currentStats.equipmentStats.Clear();

        AddItem(character.equipments.weapon, character);
        AddItem(character.equipments.helmet, character);
        AddItem(character.equipments.armor, character);
        AddItem(character.equipments.gloves, character);
        AddItem(character.equipments.shoes, character);
        AddItem(character.equipments.ring, character);
        AddItem(character.equipments.necklace, character);
        
        Debug.Log("Equipment Updated");
    }

    private void AddItem(EquipmentItem item, Character character)
    {
        if (item == null)
            return;

        character.currentStats.equipmentStats.hp += item.EquipmentStats.hp;
        character.currentStats.equipmentStats.atk += item.EquipmentStats.atk;
        character.currentStats.equipmentStats.def += item.EquipmentStats.def;
        character.currentStats.equipmentStats.speed += item.EquipmentStats.speed;
        character.currentStats.equipmentStats.critRate += item.EquipmentStats.critRate;
        character.currentStats.equipmentStats.critDamage += item.EquipmentStats.critDamage;

    }
}