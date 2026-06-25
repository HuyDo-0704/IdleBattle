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
        Debug.Log($"{attacker.name} Melee Attack {target.name}");

        Transform attackerTransform = attacker.transform; 
        Transform targetTransform = target.transform; 

        Vector3 startPos = attackerTransform.position;

        float attackDistance = 2f; // khoản cách với kẻ dịch khi tới 

        Vector3 attackPos = targetTransform.position -
                            (targetTransform.position - startPos).normalized * attackDistance;

        float moveTime = 0.2f;
        float elapsed = 0f;

        // Lướt tới
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            attackerTransform.position = Vector3.Lerp(
                startPos,
                attackPos,
                elapsed / moveTime);

            yield return null;
        }

        attackerTransform.position = attackPos;

        // Play animation
        Animator animator = attacker.Hud.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Chờ animation đánh
        yield return new WaitForSeconds(0.25f);

        int damage = DamageCalculator.CalculateDamage(attacker, target);

        target.ReceiveDamage(damage);

        Debug.Log($"{target.name} Receive {damage}");

        // Lùi về
        elapsed = 0f;

        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            attackerTransform.position = Vector3.Lerp(
                attackPos,
                startPos,
                elapsed / moveTime);

            yield return null;
        }

        attackerTransform.position = startPos;
    }

    public IEnumerator DoRangedAttack(CharacterManager attacker, CharacterManager target)
    {
        Debug.Log($"{attacker.name} Ranged Attack {target.name}");

        Animator animator = attacker.Hud.GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Chờ animation bắn
        yield return new WaitForSeconds(0.3f);

        int damage = DamageCalculator.CalculateDamage(attacker, target);

        target.ReceiveDamage(damage);

        Debug.Log($"{target.name} Receive {damage}");
    }
}