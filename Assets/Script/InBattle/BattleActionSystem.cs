using System.Collections;
using UnityEngine;

public class BattleActionSystem : MonoBehaviour
{
    public static BattleActionSystem Instance;

    [SerializeField] private float moveTime = 0.2f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackDelay = 0.25f;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator DoMeleeAttack(
        CharacterManager attacker,
        CharacterManager target,
        MeleeSkillData skill)
    {
        Transform tf = attacker.transform;

        Vector3 start = tf.position;

        Vector3 attackPos =
            target.transform.position -
            (target.transform.position - start).normalized * attackDistance;

        yield return Move(tf, start, attackPos);

        PlayAttackAnimation(attacker);

        yield return new WaitForSeconds(attackDelay);

        CombatVFXManager.Instance.SpawnHit(skill.hitVFX,target.transform);

        target.ReceiveDamage(
            DamageCalculator.CalculateDamage(attacker, target));

        yield return Move(tf, attackPos, start);
    }

    public IEnumerator DoRangedAttack(
        CharacterManager attacker,
        CharacterManager target,
        RangedSkillData skill)
    {
        PlayAttackAnimation(attacker);

        yield return new WaitForSeconds(attackDelay);

        CombatVFXManager.Instance.SpawnProjectile(attacker,target,skill);

        yield return new WaitForSeconds(0.8f);
    }

    private IEnumerator Move(Transform tf, Vector3 from, Vector3 to)
    {
        float t = 0f;

        while (t < moveTime)
        {
            t += Time.deltaTime;

            tf.position = Vector3.Lerp(from, to, t / moveTime);

            yield return null;
        }

        tf.position = to;
    }

    private void PlayAttackAnimation(CharacterManager character)
    {
        Animator anim = character.Hud.GetComponent<Animator>();

        if (anim != null)
            anim.SetTrigger("Attack");
    }
}