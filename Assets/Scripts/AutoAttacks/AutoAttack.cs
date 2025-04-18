using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Character))]
public abstract class AutoAttack : MonoBehaviour
{
    [SerializeField] protected AnimationClip attackAnimation;
    protected float attackInterval;
    protected float nextAttackTime = 0;
    protected bool canAttack = true;

    public Character target;

    protected PlayerMovement movement;
    protected Character character;
    protected Animator animator;

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

    protected abstract void Attack();

    protected virtual IEnumerator PerformAttack()
    {
        float a = Time.time;
        canAttack = false;
        animator.SetBool("AutoAttack", true);
        animator.speed = attackAnimation.length / attackInterval;

        yield return new WaitForSeconds(attackInterval);

        animator.SetBool("AutoAttack", false);
        OnAutoAttackPerformed?.Invoke(character);
        //Debug.Log($"Attack is done by {Time.time - a} seconds");
        animator.speed = 1f;
        canAttack = true;

        Attack();
    }
}
