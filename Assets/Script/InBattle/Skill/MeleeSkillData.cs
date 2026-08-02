using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Melee")]
public class MeleeSkillData : SkillData
{
    public override IEnumerator Execute(CharacterManager attacker,CharacterManager target)
    {
        yield return BattleActionSystem.Instance.DoMeleeAttack(attacker, target, this);
    }
}
