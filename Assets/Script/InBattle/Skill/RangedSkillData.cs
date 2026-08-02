using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Ranged")]
public class RangedSkillData : SkillData
{
    public GameObject projectilePrefab;

    public float projectileSpeed = 8;

    public override IEnumerator Execute(CharacterManager attacker,CharacterManager target)
    {
        yield return BattleActionSystem.Instance.DoRangedAttack(attacker, target, this);
    }
}
