using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Passives/AutoAttackOrTakeDamageStacking", fileName = "AutoAttackOrTakeDamageStacking")]
public class AutoAttackOrTakeDamageStackingEffect : ItemPassiveEffect
{
    public List<StatModifier> modifiersPerStack = new();
    public List<StatModifier> maxStackBonus = new();
    public int maxStacks = 5;
    public float stackDuration = 5f;

    private class StackData
    {
        public int currentStacks;
        public List<StatModifier> appliedModifiers = new();
        public Coroutine routine;
    }

    private Dictionary<Character, StackData> tracked = new();

    public override void OnAttack(Character owner, Character target) => TryAddStack(owner);

    public override void OnTakeDamage(Character owner, float amount) => TryAddStack(owner);

    private void TryAddStack(Character owner)
    {
        if (!tracked.TryGetValue(owner, out var data))
        {
            data = new StackData();
            tracked[owner] = data;
        }

        if (data.currentStacks >= maxStacks) return;

        data.currentStacks++;

        foreach (var mod in modifiersPerStack)
        {
            var newMod = new StatModifier
            {
                type = mod.type,
                value = mod.value,
                modifierType = mod.modifierType
            };

            StatModifyManager.Apply(owner, newMod);
            data.appliedModifiers.Add(newMod);
        }

        if (data.currentStacks == maxStacks)
        {
            foreach (var bonus in maxStackBonus)
            {
                var bonusMod = new StatModifier
                {
                    type = bonus.type,
                    value = bonus.value,
                    modifierType = bonus.modifierType
                };

                StatModifyManager.Apply(owner, bonusMod);
                data.appliedModifiers.Add(bonusMod);
            }
        }

        if (data.routine != null) owner.StopCoroutine(data.routine);
        data.routine = owner.StartCoroutine(RemoveAfterDuration(owner, stackDuration));
    }

    private IEnumerator RemoveAfterDuration(Character owner, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (!tracked.TryGetValue(owner, out var data)) yield break;

        foreach (var mod in data.appliedModifiers)
        {
            StatModifyManager.Remove(owner, mod);
        }

        tracked.Remove(owner);
    }
}
