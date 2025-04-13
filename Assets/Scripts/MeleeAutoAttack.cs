using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Character))]
public class MeleeAutoAttack : MonoBehaviour
{
    [SerializeField] protected AnimationClip attackAnimation;
    protected float attackInterval;
    protected float nextAttackTime = 0;
    protected bool canAttack = true;
    public Character target;

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

        target.ApplyDamage(new Damage(character.attackDamage.Current));
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
