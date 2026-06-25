using UnityEngine;
using System.Collections;
public enum State { Idle , Combat}
public class CharacterManager : MonoBehaviour
{
    public HudSystem Hud;
    public CurrentStats Stats;
    public State currentState;
    public void Start ()
    {
        CheckState ();
    }
    void CheckState ()
    {
        if (currentState == State.Idle)
        {
            Hud.HUDBar.gameObject.SetActive(false);
        }
    }
    public void SetupCharacter(Character character)
    {
        Stats = character.currentStats;
        
        Hud.maxHealth = Stats.MHealth;
        Hud.currentHealth = Stats.MHealth;
        currentState = State.Combat;
    }
    public IEnumerator Acting()
    {
        if (Hud.IsDead)
        {
            Debug.Log($"{name} Dead");
            yield break;
        }

        Debug.Log($"{name} Acting");

        CharacterManager target =
            TargetingSystem.Instance
            .GetNearestAliveEnemy(this);

        if (target == null)
        {
            Debug.LogWarning($"{name} Cannot Find Target");
            yield break;
        }

        Debug.Log($"{name} Target => {target.name}");

        switch (Stats.baseStats.attackType)
        {
            case AttackType.Melee:

                yield return StartCoroutine(
                    BattleActionSystem.Instance
                    .DoMeleeAttack(this, target));

                break;

            case AttackType.Ranged:

                yield return StartCoroutine(
                    BattleActionSystem.Instance
                    .DoRangedAttack(this, target));

                break;
        }
        Hud.RestoreMana(25f);
    }

    public void ReceiveDamage(int damage)
    {
        Hud.TakeDamage(damage);
         DamagePopupManager.Instance.ShowDamage(Hud.transform, damage);
        Debug.Log(
            $"{name} HP = {Hud.currentHealth}/{Hud.maxHealth}");
    }

}
