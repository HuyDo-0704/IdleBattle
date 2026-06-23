using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BattleTeam : MonoBehaviour
{
    public List<CharacterManager> members;

    public bool HasAliveMember()
    {
        foreach (var member in members)
        {
            if (!member.Hud.IsDead)
                return true;
        }

        return false;
    }
}
