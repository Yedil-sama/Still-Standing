using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/Passive Item", fileName = "Item")]
public class PassiveItem : StatItem
{
    [SerializeField] private List<ItemPassiveEffect> effects = new();

    public override void OnEquip(Character owner)
    {
        base.OnEquip(owner);
        foreach (var effect in effects)
            effect?.OnEquip(owner);
    }

    public override void OnUnequip(Character owner)
    {
        base.OnUnequip(owner);
        foreach (var effect in effects)
            effect?.OnUnequip(owner);
    }

    public void OnAttack(Character owner, Character target)
    {
        foreach (var effect in effects)
            effect?.OnAttack(owner, target);
    }

    public void OnHit(Character owner, Character target, Damage damageDealt)
    {
        foreach (var effect in effects)
            effect?.OnHit(owner, target, damageDealt);
    }

    public void OnTakeDamage(Character owner, float amount)
    {
        foreach (var effect in effects)
            effect?.OnTakeDamage(owner, amount);
    }

    public void Tick(Character owner, float deltaTime)
    {
        foreach (var effect in effects)
            effect?.Tick(owner, deltaTime);
    }
}
