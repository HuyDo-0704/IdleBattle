using UnityEngine;

public static class StarCalculator
{
    public static int CalculateStars(StageData stage)
    {
        int stars = 0;

        // BattleWin() chỉ được gọi khi đã thắng
        // nên không cần cộng 1 sao mặc định nữa

        if (CheckAlive(stage))
            stars++;

        if (CheckRemainHP(stage))
            stars++;

        if (CheckRound(stage))
            stars++;

        return stars;
    }

    private static bool CheckRound(StageData stage)
    {
        return BattleManager.Instance.turnManager.CurrentRound
            <= stage.requireMaxRound;
    }

    private static bool CheckAlive(StageData stage)
    {
        int alive = 0;

        foreach (CharacterManager c in BattleManager.Instance.playerTeam.members)
        {
            if (!c.Hud.IsDead)
                alive++;
        }

        return alive >= stage.requireAliveCharacter;
    }

    private static bool CheckRemainHP(StageData stage)
    {
        float current = 0f;
        float max = 0f;

        foreach (CharacterManager c in BattleManager.Instance.playerTeam.members)
        {
            current += c.Hud.currentHealth;
            max += c.Hud.maxHealth;
        }

        if (max <= 0)
            return false;

        float percent = current / max * 100f;

        return percent >= stage.requireRemainHPPercent;
    }
}