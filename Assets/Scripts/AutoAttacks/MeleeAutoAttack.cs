using UnityEngine;

public class MeleeAutoAttack : AutoAttack
{
    protected override void Attack()
    {
        if (target == null) return;

        target.ApplyDamage(new Damage(character.attackDamage.Current));
        nextAttackTime = Time.time + attackInterval;

        animator.SetBool("AutoAttack", false);
    }
}
