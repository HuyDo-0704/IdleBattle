using UnityEngine;

public class CombatVFXManager : MonoBehaviour
{
    public static CombatVFXManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnHit(GameObject hitVFX, Transform target)
    {
        if (hitVFX == null || target == null)
            return;

        Instantiate(hitVFX, target.position, Quaternion.identity);
    }

    public void SpawnProjectile(CharacterManager attacker, CharacterManager target, RangedSkillData skillData)
    {
        if (skillData == null || skillData.projectilePrefab == null)
            return;

        GameObject obj = Instantiate(skillData.projectilePrefab, attacker.FirePoint.position, Quaternion.identity);

        Projectile projectile = obj.GetComponent<Projectile>();

        if (projectile != null)
            projectile.Setup(attacker, target, skillData);
    }
}