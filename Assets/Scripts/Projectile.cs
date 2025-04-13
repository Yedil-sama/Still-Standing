using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Damage damage;
    public float speed;
    private Transform target;
    private Transform originalTarget;

    public void Start()
    {
        originalTarget = target;
    }

    public void Update()
    {
        if (target != null)
        {
            MoveTowards(target);
        }
        else if (originalTarget != null)
        {
            MoveTowards(originalTarget);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void MoveTowards(Transform moveTarget)
    {
        Vector3 direction = (moveTarget.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(moveTarget);
    }
    private void OnTriggerEnter(Collider other)
    {
        Character hitCharacter = other.GetComponent<Character>();
        if (hitCharacter == null) return;
        if (target != null && hitCharacter == target.GetComponent<Character>())
        {
            hitCharacter.ApplyDamage(damage);
            Destroy(gameObject);
        }
        else if (originalTarget != null && hitCharacter == originalTarget.GetComponent<Character>())
        {
            hitCharacter.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }



}
