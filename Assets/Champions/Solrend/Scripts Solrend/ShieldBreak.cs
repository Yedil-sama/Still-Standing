using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player Ability/Shield Break", fileName = "Shield Break")]
public class ShieldBreak : PlayerAbility
{
    [Header("Shield Break")]
    public float radius;
    public float armorReduction;
    public float effectDuration;

    public override void Activate()
    {
        Collider[] others = Physics.OverlapSphere(owner.transform.position, radius);

        foreach (Collider other in others)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                owner.DealDamage(new Damage(baseAmount + owner.attackDamage.Current * attackDamageScale + owner.spellDamage.Current * spellDamageScale, DamageType.Magical), enemy);
                owner.StartCoroutine(DoReduceArmor(enemy));
            }
        }
    }

    private IEnumerator DoReduceArmor(Enemy enemy)
    {
        enemy.armor.Current -= armorReduction;

        yield return new WaitForSeconds(effectDuration);

        enemy.armor.Current += armorReduction;
    }
}
