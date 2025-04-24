using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/Hybrid Item", fileName = "Item")]
public class HybridItem : StatItem, IActivatable
{
    [SerializeField] private ItemPassiveEffect[] passiveEffects;
    [SerializeField] private ItemActiveEffect activeEffect;

    public override void OnEquip(Character owner)
    {
        base.OnEquip(owner);

        foreach (var effect in passiveEffects)
        {
            effect?.OnEquip(owner);
        }
    }

    public override void OnUnequip(Character owner)
    {
        base.OnUnequip(owner);

        foreach (var effect in passiveEffects)
        {
            effect?.OnUnequip(owner);
        }
    }

    public void Activate(Character owner, Character target) => activeEffect?.TryActivate(owner, target);

    public bool CanActivate(Character owner) => activeEffect != null && activeEffect.CanActivate(owner);

    public string GetEffectName() => activeEffect?.effectName ?? "None";
    public float GetCooldown() => activeEffect?.cooldown ?? 0f;
}
