using UnityEngine;

[System.Serializable]
public class CurrentStats
{
    public CharacterBaseStats baseStats;
    public Stats equipmentStats;
    public Stats FinalStats;
    [Header("Progression")]
    public float PowerStats;

    public void RecalculateStats(int CurrentLevel)
    {
        FinalStats.hp = baseStats.GetHealthByLevel(CurrentLevel) + equipmentStats.hp ;
        FinalStats.atk = baseStats.GetAttackByLevel(CurrentLevel) + equipmentStats.atk;
        FinalStats.def = baseStats.GetDefenseByLevel(CurrentLevel) + equipmentStats.def;
        FinalStats.manaBonus = baseStats.GetManaBonusByLevel(CurrentLevel) + equipmentStats.manaBonus;
        FinalStats.speed = baseStats.Speed + equipmentStats.speed;
        FinalStats.critRate = baseStats.CriticalRate + equipmentStats.critRate;
        FinalStats.critDamage = baseStats.CriticalDamage + equipmentStats.critDamage;

        UpdatePower();
    }

    // CT: ATK×1.2 + DEF×1 + HP×0.1 + SPD×2 + CR×15 + CD×8
    public void UpdatePower()
    {
        PowerStats =
            FinalStats.atk * 1.2f +
            FinalStats.def * 1f +
            FinalStats.hp * 0.1f +
            FinalStats.speed * 2f +
            FinalStats.critRate * 15f +
            FinalStats.critDamage * 8f;
    }
}
