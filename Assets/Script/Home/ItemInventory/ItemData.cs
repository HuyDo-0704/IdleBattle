using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Helmet,
    Armor,
    Gloves,
    Boots,
    Ring,
    Necklace
}

public enum ItemRare
{
    All, // dùng cho item 1 ảnh 
    Common,
    Rare,
    Epic,
    Legendary,
    Mythic
}

[CreateAssetMenu(menuName = "Idle RPG/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public int itemID;
    public string itemName;
    public List<IconItem> icon; // ảnh tùy theo rare

    [TextArea]
    public string description;

    [Header("Type")]
    public ItemType itemType;

    [Header("Base Stats")]
    public int hp;
    public int atk;
    public int def;
    public int speed;
    public int critRate;
    public int critDamage;
    // hàm để tìm icon itemitem
    public Sprite GetSpriteIcon(ItemRare rare)
    {
        return icon.Find(x => x.rare == rare)?.icon
            ?? icon.Find(x => x.rare == ItemRare.All)?.icon;
    }
}

[System.Serializable]
public class IconItem
{
    public Sprite icon;
    public ItemRare rare;
}
