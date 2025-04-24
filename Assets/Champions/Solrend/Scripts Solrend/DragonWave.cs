using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player Ability/Dragon Wave", fileName = "Dragon Wave")]
public class DragonWave : PlayerAbility
{
    [Header("Dragon Wave")]
    public float damageIncreasePerHit;
    public float projectileSpeed;
    public float projectileLifeTime;
    public GameObject projectilePrefab;

    public override void Activate()
    {
        if (projectilePrefab == null || owner == null) return;

        Vector3 spawnPosition = owner.rootTransform.position;
        Vector3 direction = (GameManager.Instance.GetMousePosition() - owner.transform.position).normalized;

        GameObject projectileGO = GameObject.Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(direction));

        if (projectileGO.TryGetComponent(out DragonWaveProjectile projectile))
        {
            Damage damage = new Damage(baseAmount + attackDamageScale * owner.attackDamage.Current + spellDamageScale * owner.spellDamage.Current);
            projectile.Initialize(damage, projectileSpeed);
            projectile.lifetime = projectileLifeTime;
            projectile.damageIncreasePerHit = damageIncreasePerHit;
            projectile.owner = owner;
            projectile.Launch(direction);
        }
    }
}
