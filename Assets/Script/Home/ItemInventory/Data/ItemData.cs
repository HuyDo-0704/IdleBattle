using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Equipment,       // Trang bị
    Consumable,      // Vật phẩm sử dụng
    Material,        // Nguyên liệu
    Chest,           // Rương
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
public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    public int itemID;
    public string itemName;

    [TextArea]
    public string description;

    public ItemType itemType;

    public List<IconItem> icons;

    public Sprite GetSpriteIcon(ItemRare rare)
    {
        return icons.Find(x => x.rare == rare)?.icon
            ?? icons.Find(x => x.rare == ItemRare.All)?.icon;
    }
}

[System.Serializable]
public class IconItem
{
    public Sprite icon;
    public ItemRare rare;
}
