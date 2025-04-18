using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Character))]
public abstract class AutoAttack : MonoBehaviour
{
    public Character target;
    [SerializeField, Range(0f, 1f)] protected float attackWindupPercent = 0.7f;
    [SerializeField] protected AnimationClip attackAnimation;
    protected float attackInterval;
    protected float nextAttackTime = 0;
    protected bool canAttack = true;

    protected PlayerMovement movement;
    protected Character character;
    protected Animator animator;

    private Coroutine attackCoroutine;
    private bool isInWindup = false;

    public event Action<Character> OnAutoAttackPerformed;

    public virtual void Start()
    {
        movement = GetComponent<PlayerMovement>();
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        attackInterval = character.attackSpeed.baseAttackTime / (1 + character.attackSpeed.Current / 100f);

        if (isInWindup && movement.agent.velocity.magnitude > 0.1f)
        {
            CancelAttack();
        }

        if (canAttack && Time.time > nextAttackTime)
        {
            if (movement.target != null && movement.target.TryGetComponent<Character>(out target))
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= movement.stoppingDistance)
                {
                    attackCoroutine = StartCoroutine(PerformAttack());
                }
            }
        }
    }

    protected abstract void Attack();

    protected virtual IEnumerator PerformAttack()
    {
        canAttack = false;
        isInWindup = true;

        animator.SetBool("AutoAttack", true);

        float windupTime = attackAnimation.length * attackWindupPercent;
        float animSpeed = attackAnimation.length / attackInterval;
        animator.speed = animSpeed;

        yield return new WaitForSeconds(windupTime / animSpeed);

        if (!isInWindup) yield break;

        Attack();
        OnAutoAttackPerformed?.Invoke(target);

        isInWindup = false;

        float remainingTime = attackInterval - windupTime;
        if (remainingTime > 0)
            yield return new WaitForSeconds(remainingTime / animSpeed);

        animator.SetBool("AutoAttack", false);
        animator.speed = 1f;

        nextAttackTime = Time.time + attackInterval;
        canAttack = true;
    }

    protected virtual void CancelAttack()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        animator.SetBool("AutoAttack", false);
        animator.speed = 1f;

        isInWindup = false;
        canAttack = true;
        attackCoroutine = null;

        nextAttackTime = Time.time + 0.1f;
    }
}