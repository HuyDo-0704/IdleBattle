using UnityEngine;

public class Projectile : MonoBehaviour
{
    private CharacterManager attacker;
    private CharacterManager target;

    private RangedSkillData skillData;

    public void Setup(CharacterManager attacker, CharacterManager target, RangedSkillData skillData)
    {
        this.attacker = attacker;
        this.target = target;
        this.skillData = skillData;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            skillData.projectileSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        // Spawn Hit VFX
        if (skillData.hitVFX != null)
        {
            Instantiate(
                skillData.hitVFX,
                target.transform.position,
                Quaternion.identity);
        }

        // Damage
        target.ReceiveDamage(skillData.damage);

        Destroy(gameObject);
    }
}