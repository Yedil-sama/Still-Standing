using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Passives/StatLinkEffect", fileName = "StatLinkEffect")]
public class StatLinkEffect : ItemPassiveEffect
{
    [Serializable]
    public class BuffEntry
    {
        public StatType sourceStat;
        public StatType targetStat;
        public float multiplier = 1f;
        public ModifierType modifierType = ModifierType.Flat;
    }

    public List<BuffEntry> buffs = new();

    private Dictionary<Character, List<StatModifier>> activeModifiers = new();

    public override void OnEquip(Character owner)
    {
        ApplyBuffs(owner);
        owner.OnStatChanged += OnStatChanged;
    }

    public override void OnUnequip(Character owner)
    {
        RemoveBuffs(owner);
        owner.OnStatChanged -= OnStatChanged;
    }

    private void OnStatChanged(Character owner, StatType changedStat)
    {
        foreach (var buff in buffs)
        {
            if (buff.sourceStat == changedStat)
            {
                ApplyBuffs(owner);
                break;
            }
        }
    }

    private void ApplyBuffs(Character owner)
    {
        RemoveBuffs(owner);

        List<StatModifier> modifiers = new();

        foreach (var buff in buffs)
        {
            float sourceValue = owner.GetStat(buff.sourceStat);
            float value = sourceValue * buff.multiplier;

            var mod = new StatModifier
            {
                type = buff.targetStat,
                value = value,
                modifierType = buff.modifierType
            };

            StatModifyManager.Apply(owner, mod);
            modifiers.Add(mod);
        }

        activeModifiers[owner] = modifiers;
    }

    private void RemoveBuffs(Character owner)
    {
        if (!activeModifiers.TryGetValue(owner, out var mods)) return;

        foreach (var mod in mods)
        {
            StatModifyManager.Remove(owner, mod);
        }

        activeModifiers.Remove(owner);
    }
}
