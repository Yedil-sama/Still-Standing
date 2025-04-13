using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Character))]
public class RangeAutoAttack : MonoBehaviour
{

    public Character target;
    [SerializeField] protected AnimationClip attackAnimation;

    [Header("ProjectileSettings")]
    public float projectileSpeed;
    public Transform spawnTransform;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject projectilePrefab;
    protected Damage projectileDamage;

    protected float attackInterval;
    protected float nextAttackTime = 0;
    protected bool canAttack = true;

    protected PlayerMovement movement;
    protected Character character;
    protected Animator animator;

    public void Start()
    {
        movement = GetComponent<PlayerMovement>();
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        attackInterval = character.attackSpeed.baseAttackTime / (1 + character.attackSpeed.Current / 100f);

        if (movement.target != null && movement.target.TryGetComponent<Character>(out target))
        {
            if (canAttack && Time.time > nextAttackTime)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= movement.stoppingDistance)
                {
                    StartCoroutine(PerformAttack());
                }
            }
        }
    }

    protected virtual void Attack()
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

    protected IEnumerator PerformAttack()
    {
        float a = Time.time;
        canAttack = false;
        animator.SetBool("AutoAttack", true);
        animator.speed = attackAnimation.length / attackInterval;

        yield return new WaitForSeconds(attackInterval);

        animator.SetBool("AutoAttack", false);
        Debug.Log($"Attack is done by {Time.time - a} seconds");
        animator.speed = 1f;
        canAttack = true;
    }
}
