using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement), typeof(Character))]
public abstract class AutoAttack : MonoBehaviour
{
    public Character target;
    [SerializeField, Range(0f, 1f)] protected float attackWindupPercent = 0.7f;
    [SerializeField] protected AnimationClip attackAnimation;
    protected float attackInterval;
    protected float nextAttackTime = 0;
    protected bool canAttack = true;

    protected CharacterMovement movement;
    protected Character character;
    protected Animator animator;

    private Coroutine attackCoroutine;
    private bool isInWindup = false;

    public event Action<Character> OnAutoAttackPerformed;

    public virtual void Start()
    {
        movement = GetComponent<CharacterMovement>();
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        attackInterval = character.attackSpeed.baseAttackTime / (1 + character.attackSpeed.Current / 100f);

        if (movement.target != null && movement.target.TryGetComponent<Character>(out target))
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= movement.stoppingDistance)
            {
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(DoAutoAttack());
                }
            }
            else
            {
                CancelAttack();
            }
        }
        else
        {
            CancelAttack();
        }
    }

    protected abstract void Attack();

    protected virtual IEnumerator DoAutoAttack()
    {
        while (target != null && Vector3.Distance(transform.position, target.transform.position) <= movement.stoppingDistance)
        {
            canAttack = false;
            isInWindup = true;

            character.LookAt(target.transform.position);

            float windupTime = attackAnimation.length * attackWindupPercent;
            float animSpeed = attackAnimation.length / attackInterval;

            if (animator != null)
            {
                animator.speed = animSpeed;
                animator.SetBool("AutoAttack", true);
            }

            yield return new WaitForSeconds(windupTime / animSpeed);

            if (!isInWindup)
            {
                break;
            }

            Attack();
            OnAutoAttackPerformed?.Invoke(target);

            isInWindup = false;

            float remainingTime = attackInterval - windupTime;
            if (remainingTime > 0)
                yield return new WaitForSeconds(remainingTime / animSpeed);

            if (animator != null)
            {
                animator.SetBool("AutoAttack", false);
                animator.speed = 1f;
            }

            nextAttackTime = Time.time + attackInterval;
            canAttack = true;
        }

        CancelAttack();
    }

    protected virtual void CancelAttack()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        if (animator != null)
        {
            animator.SetBool("AutoAttack", false);
            animator.speed = 1f;
        }

        isInWindup = false;
        canAttack = true;
        attackCoroutine = null;

        nextAttackTime = Time.time + 0.1f;
    }
}