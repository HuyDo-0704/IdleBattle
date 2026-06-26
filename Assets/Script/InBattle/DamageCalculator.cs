using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(
        CharacterManager attacker,
        CharacterManager defender)
    {
        float attack =
            attacker.Stats.CAttack;

        float defense =
            defender.Stats.CDef;

        float damage =
            Mathf.Max(1, attack - defense);

        return (int)damage;
    }
}