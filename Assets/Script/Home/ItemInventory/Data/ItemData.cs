using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
    public ItemRare defaultRare;
    public Sprite icons;

}
