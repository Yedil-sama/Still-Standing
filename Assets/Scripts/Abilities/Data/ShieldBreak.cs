using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player Ability/Shield Break", fileName = "New Shield Break")]
public class ShieldBreak : PlayerAbility
{
    [Header("Shield Break")]
    public float radius = 5f;
    public float armorReduction = 10f;
    public float effectDuration = 5f;

    public override void Activate()
    {
        Collider[] others = Physics.OverlapSphere(owner.transform.position, radius);

        foreach (Collider other in others)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.ApplyDamage(new Damage(baseAmount + (owner.attackDamage.Current * attackDamageScale) + (owner.spellDamage.Current * spellDamageScale), DamageType.Magical));
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
