using UnityEngine;

public class HomingProjectile : Projectile
{
    public void SetTarget(Character newTarget)
    {
        target = newTarget;
    }

    protected override void Move()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.rootTransform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(target.rootTransform);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (target != null && other.transform == target.transform)
        {
            owner.DealDamage(damage, target);
            Destroy(gameObject);
        }
    }
}
