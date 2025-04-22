using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public Damage damage;
    public float lifetime = 5f;
    protected Character target;
    private float timeAlive = 0f;

    public float maxDistance = 50f;
    public Character owner;
    private Vector3 spawnPosition;

    protected virtual void Start()
    {
        spawnPosition = transform.position;
    }

    protected virtual void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(spawnPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }

        Move();
    }

    protected abstract void Move();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (target != null && other.GetComponent<Character>() != owner)
        {
            owner.DealDamage(damage, target);
            Destroy(gameObject);
        }
    }
}
