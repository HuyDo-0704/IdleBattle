using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public static TargetingSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public CharacterManager GetNearestAliveEnemy(
        CharacterManager attacker)
    {
        BattleTeam enemyTeam =
            BattleManager.Instance.GetEnemyTeam(attacker);

        CharacterManager nearest = null;

        float nearestDistance = float.MaxValue;

        foreach (var enemy in enemyTeam.members)
        {
            if (enemy.Hud.IsDead)
                continue;

            float distance =
                Vector3.Distance(
                    attacker.transform.position,
                    enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }
}