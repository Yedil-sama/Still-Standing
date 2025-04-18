using UnityEngine;

public class RangeAutoAttack : AutoAttack
{
    [Header("ProjectileSettings")]
    public float projectileSpeed;
    public Transform spawnTransform;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject projectilePrefab;
    protected Damage projectileDamage;

    protected override void Attack()
    {
        if (target == null) return;

        projectile = Instantiate(projectilePrefab, spawnTransform.position, spawnTransform.rotation);

        Projectile proj = projectile.GetComponent<Projectile>();

        if (proj != null)
        {
            proj.speed = projectileSpeed;
            proj.damage = new Damage(character.attackDamage.Current);
            proj.SetTarget(target.transform);
        }

        nextAttackTime = Time.time + attackInterval;

        animator.SetBool("AutoAttack", false);
    }
}
