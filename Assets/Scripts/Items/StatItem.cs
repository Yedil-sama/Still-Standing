using UnityEngine;

public class StatItem : Item, IStatProvider
{
    [SerializeField] private StatModifier[] statModifiers;

    public override void OnEquip(Character owner)
    {
        StatModifyManager.ApplyAll(owner, statModifiers);
    }

    public override void OnUnequip(Character owner)
    {
        StatModifyManager.RemoveAll(owner, statModifiers);
    }

    public StatModifier[] GetStatModifiers() => statModifiers;
}
