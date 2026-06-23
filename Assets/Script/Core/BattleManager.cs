using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public BattleTeam playerTeam;
    public BattleTeam enemyTeam;

    private void Awake()
    {
        Instance = this;
    }

    public BattleTeam GetEnemyTeam(
        CharacterManager character)
    {
        if (playerTeam.members.Contains(character))
            return enemyTeam;

        return playerTeam;
    }

    public bool CheckBattleEnd()
    {
        if (!playerTeam.HasAliveMember())
            return true;

        if (!enemyTeam.HasAliveMember())
            return true;

        return false;
    }
}