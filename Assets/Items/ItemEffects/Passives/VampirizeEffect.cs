using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Passives/Vampirize", fileName = "Vampirize")]
public class VampirizeEffect : ItemPassiveEffect
{
    public float flatHeal = 5f;
    public float percentageHeal = 0.1f;

    public override void OnHit(Character owner, Character target, Damage damageDealt)
    {
        float healAmount = flatHeal + damageDealt.amount * percentageHeal;
        owner.health.Current += healAmount;
        owner.health.ClampCurrent();
    }
}
