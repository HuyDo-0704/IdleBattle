using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(
        CharacterManager attacker,
        CharacterManager defender)
    {
        float attack =
            attacker.Stats.baseStats.DeAttack;

        float defense =
            defender.Stats.baseStats.DeDefense;

        float damage =
            Mathf.Max(1, attack - defense);

        return (int)damage;
    }
}