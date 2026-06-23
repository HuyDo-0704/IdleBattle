using System.Collections;
using UnityEngine;

public class BattleActionSystem : MonoBehaviour
{
    public static BattleActionSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator DoMeleeAttack(CharacterManager attacker, CharacterManager target)
    {
        Debug.Log(
            $"{attacker.name} Melee Attack {target.name}");

        yield return new WaitForSeconds(0.5f);

        int damage =
            DamageCalculator.CalculateDamage(
                attacker,
                target);

        target.ReceiveDamage(damage);

        Debug.Log(
            $"{target.name} Receive {damage}");

        yield return null;
    }

    public IEnumerator DoRangedAttack(CharacterManager attacker, CharacterManager target)
    {
        Debug.Log(
            $"{attacker.name} Ranged Attack {target.name}");

        yield return new WaitForSeconds(0.5f);

        int damage =
            DamageCalculator.CalculateDamage(
                attacker,
                target);

        target.ReceiveDamage(damage);
    }
}