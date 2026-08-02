using System.Collections;
using UnityEngine;
public enum SkillType
{
    Normal,
    Ultimate
}
public abstract class SkillData : ScriptableObject
{
    public string skillName;
    public SkillType skillType;

    public int damage;

    public GameObject castVFX;
    public GameObject hitVFX;
    public abstract IEnumerator Execute(CharacterManager attacker, CharacterManager target);
}