using UnityEngine;

public class DirectionalProjectile : Projectile
{
    private Vector3 direction;

    public void Initialize(Damage damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    public void Launch(Vector3 direction)
    {
        this.direction = direction;
    }

    protected override void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
