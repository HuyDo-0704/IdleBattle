using UnityEngine;
using System.Linq;
using System;
public enum Realm
{ // đổi tên vương quốc sau khi có ý tưởng
    wind,
    glass,
    earth,
    electric,
}
// 
[CreateAssetMenu(fileName = "DataGame", menuName = "Game/DataGame")]
public class DataGame : ScriptableObject
{
    public CharacterBaseStats[] characterBaseStats;
    [Header("UI")]
    public InfoStar[] UIStar;
    public ColorRareItem[] colorRares;
    public ItemDatabase itemDatabase;

    public CharacterBaseStats GetCharacter(string characterID)
    {
        return characterBaseStats.FirstOrDefault(c =>
            c != null &&
            c.characterID.Equals(characterID, StringComparison.OrdinalIgnoreCase));
    }
    public Sprite GetSpriteStar(StarType type)
    {
        var star = UIStar.FirstOrDefault(s => s.starType == type);

        if (star == null)
        {
            Debug.LogWarning($"Star sprite not found: {type}");
            return null;
        }

        return star.ImageStar;
    }
    public Color GetColorByRarity(ItemRare rarity)
    {
        var colorRare = colorRares.FirstOrDefault(c => c.rarity == rarity);
        if (colorRare == null)
        {
            Debug.LogWarning($"Color for rarity {rarity} not found.");
            return Color.white; // Return a default color if not found
        }
        return colorRare.color;
    }
    
}

public enum StarType
{
    Empty,
    Yellow,
    Red,
    Diamond
}
[System.Serializable]
public class InfoStar
{
    public StarType starType;
    public Sprite ImageStar;
}
[System.Serializable]
public class ColorRareItem
{
    public Color color;
    public ItemRare rarity;
}

