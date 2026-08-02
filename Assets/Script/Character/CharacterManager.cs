using UnityEngine;
using System.Collections;

public enum State { Idle, Combat }

public class CharacterManager : MonoBehaviour
{
    public HudSystem Hud;
    public Character CChartacter;
    public State currentState;

    [Header("Combat")]
    [SerializeField] private Transform firePoint;
    public Transform FirePoint => firePoint;

    private void Start()
    {
        CheckState();
    }

    public void CheckState()
    {
        if (currentState == State.Idle)
        {
            Hud.HUDBar.gameObject.SetActive(false);
        }
    }

    public void SetupCharacter(Character character)
    {
        CChartacter = character;

        Hud.maxHealth = CChartacter.currentStats.FinalStats.hp;
        Hud.currentHealth = CChartacter.currentStats.FinalStats.hp;
        currentState = State.Combat;
    }

    public IEnumerator Acting()
    {
        if (Hud.IsDead)
            yield break;

        CharacterManager target =
            TargetingSystem.Instance.GetNearestAliveEnemy(this);

        if (target == null)
            yield break;

        yield return CChartacter.currentStats
            .baseStats
            .normalAttack
            .Execute(this, target);

        Hud.RestoreMana(25f);
    }

    public void ReceiveDamage(int damage)
    {
        Hud.TakeDamage(damage);
        DamagePopupManager.Instance.ShowDamage(Hud.transform, damage);

        Debug.Log($"{name} HP = {Hud.currentHealth}/{Hud.maxHealth}");
    }
}