using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(
        CharacterManager attacker,
        CharacterManager defender)
    {
        float attack =
            attacker.CChartacter.currentStats.FinalStats.atk;

        float defense =
            defender.CChartacter.currentStats.FinalStats.def;

        float damage =
            Mathf.Max(1, attack - defense);

        return (int)damage;
    }
}