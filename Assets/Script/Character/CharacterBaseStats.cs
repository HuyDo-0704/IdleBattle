using UnityEngine;
public enum AttackType
{
    Melee,
    Ranged,
}
[CreateAssetMenu(fileName = "CharacterBaseStats", menuName = "RPG/Character Base Stats")]
public class CharacterBaseStats : ScriptableObject
{
    [Header("Basic Info")]
    public Realm realm;
    public string characterID; // Unique identifier for the character
    public string characterName;
    public Sprite characterIcon;
    public GameObject characterPrefab;
    public Rare rare;

    [Header("Base Stats")]
    public float DeHealth = 100f;
    public float DeAttack = 10f;
    public float DeDefense = 5f;
    public float ManaBonus = 0f;
    public float Speed = 3.5f;
    public float CriticalRate = 0f; 
    public float CriticalDamage = 0f;

    [Header("Attack Settings")]
    public AttackType attackType = AttackType.Melee;
    public float attackSpeed = 1f; // attacks per second

    [Header("Level Scaling")]
    public float healthPerLevel = 10;
    public float manaPerLevel = 5;
    public float attackPerLevel = 2;
    public float defensePerLevel = 1;
    private int baseLevel = 1;

    // --------- Optional: Function to get stats by level ----------
    public float GetHealthByLevel(float level)
    {
        return DeHealth + (level - baseLevel) * healthPerLevel;
    }

    public float GetManaBonusByLevel(float level)
    {
        return ManaBonus + (level - baseLevel) * manaPerLevel;
    }

    public float GetAttackByLevel(float level)
    {
        return DeAttack + (level - baseLevel) * attackPerLevel;
    }

    public float GetDefenseByLevel(float level)
    {
        return DeDefense + (level - baseLevel) * defensePerLevel;
    }
    
}
public enum Rarity
{
    C,
    B,
    A,
    S,
    SS
}

[System.Serializable]
public class Rare
{
    public Rarity rarity;
}