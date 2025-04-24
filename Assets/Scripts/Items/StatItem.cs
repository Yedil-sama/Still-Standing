using UnityEngine;

public class StatItem : Item, IStatProvider
{
    [SerializeField] private StatModifier[] statModifiers;

    public override void OnEquip(Character owner)
    {
        foreach (StatModifier bonus in statModifiers)
        {
            ApplyBonus(owner, bonus);
        }
    }

    public override void OnUnequip(Character owner)
    {
        foreach (StatModifier bonus in statModifiers)
        {
            RemoveBonus(owner, bonus);
        }
    }

    public StatModifier[] GetStatModifiers() => statModifiers;

    private void ApplyBonus(Character owner, StatModifier bonus)
    {
        switch (bonus.type)
        {
            case StatType.Health:
                owner.health.Bonus += bonus.value;
                break;
            case StatType.Mana:
                owner.mana.Bonus += bonus.value;
                break;
            case StatType.Armor:
                owner.armor.Bonus += bonus.value;
                break;
            case StatType.MagicResistance:
                owner.magicResistance.Bonus += bonus.value;
                break;
            case StatType.MovementSpeed:
                owner.speed.Bonus += bonus.value;
                break;
            case StatType.AttackDamage:
                owner.attackDamage.Bonus += bonus.value;
                break;
            case StatType.AttackSpeed:
                owner.attackSpeed.Bonus += bonus.value;
                break;
            case StatType.SpellDamage:
                owner.spellDamage.Bonus += bonus.value;
                break;
        }
    }

    private void RemoveBonus(Character owner, StatModifier bonus)
    {
        switch (bonus.type)
        {
            case StatType.Health:
                owner.health.Bonus -= bonus.value;
                break;
            case StatType.Mana:
                owner.mana.Bonus -= bonus.value;
                break;
            case StatType.Armor:
                owner.armor.Bonus -= bonus.value;
                break;
            case StatType.MagicResistance:
                owner.magicResistance.Bonus -= bonus.value;
                break;
            case StatType.MovementSpeed:
                owner.speed.Bonus -= bonus.value;
                break;
            case StatType.AttackDamage:
                owner.attackDamage.Bonus -= bonus.value;
                break;
            case StatType.AttackSpeed:
                owner.attackSpeed.Bonus -= bonus.value;
                break;
            case StatType.SpellDamage:
                owner.spellDamage.Bonus -= bonus.value;
                break;
        }
    }
}
