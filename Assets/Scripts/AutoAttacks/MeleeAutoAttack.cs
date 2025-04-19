using UnityEngine;

public class MeleeAutoAttack : AutoAttack
{
    protected override void Attack()
    {
        if (target == null) return;

        character.DealDamage(new Damage(character.attackDamage.Current), target);
        nextAttackTime = Time.time + attackInterval;

        animator.SetBool("AutoAttack", false);
    }
}
