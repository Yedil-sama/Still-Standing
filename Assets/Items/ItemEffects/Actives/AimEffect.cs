using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Actives/Aim", fileName = "Aim")]
public class AimEffect : ItemActiveEffect
{
    [SerializeField] private float baseDamage = 50f;
    [SerializeField] private float attackDamageScale = 0.5f;
    [SerializeField] private float spellDamageScale = 0.3f;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowPercent = 0.3f;

    protected override void Activate(Character owner, Character target)
    {
        float damageAmount = baseDamage + (owner.attackDamage.Current * attackDamageScale + owner.spellDamage.Current * spellDamageScale);
        var damage = new Damage(damageAmount, DamageType.Physical);

        owner.DealDamage(damage, target);

        if (target.speed != null)
        {
            target.StartCoroutine(ApplySlow(target));
        }
    }

    private IEnumerator ApplySlow(Character target)
    {
        float originalSpeed = target.speed.Current;
        target.speed.Current *= (1f - slowPercent);
        yield return new WaitForSeconds(slowDuration);
        target.speed.Current = originalSpeed;
    }
}
