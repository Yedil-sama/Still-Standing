using System.Collections.Generic;

public static class StatModifyManager
{
    public static void Apply(Character target, StatModifier mod)
    {
        switch (mod.type)
        {
            case StatType.Health:
                target.health.Bonus += mod.value;
                break;
            case StatType.Mana:
                target.mana.Bonus += mod.value;
                break;
            case StatType.Armor:
                target.armor.Bonus += mod.value;
                break;
            case StatType.MagicResistance:
                target.magicResistance.Bonus += mod.value;
                break;
            case StatType.MovementSpeed:
                target.speed.Bonus += mod.value;
                break;
            case StatType.AttackDamage:
                target.attackDamage.Bonus += mod.value;
                break;
            case StatType.AttackSpeed:
                target.attackSpeed.Bonus += mod.value;
                break;
            case StatType.SpellDamage:
                target.spellDamage.Bonus += mod.value;
                break;
        }
    }

    public static void Remove(Character target, StatModifier mod)
    {
        StatModifier inverse = new StatModifier
        {
            type = mod.type,
            value = -mod.value,
            modifierType = mod.modifierType
        };

        Apply(target, inverse);
    }

    public static void ApplyAll(Character target, IEnumerable<StatModifier> mods)
    {
        foreach (var mod in mods)
        {
            Apply(target, mod);
        }
    }

    public static void RemoveAll(Character target, IEnumerable<StatModifier> mods)
    {
        foreach (var mod in mods)
        {
            Remove(target, mod);
        }
    }
}
