using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/Active Item", fileName = "Item")]
public class ActiveItem : StatItem, IActivatable
{
    [SerializeField] protected ItemActiveEffect effect;

    public override void OnEquip(Character owner)
    {
        base.OnEquip(owner);
    }

    public override void OnUnequip(Character owner)
    {
        base.OnUnequip(owner);
    }

    public void Activate(Character owner, Character target) => effect?.TryActivate(owner, target);

    public bool CanActivate(Character owner) => effect != null && effect.CanActivate(owner);

    public string GetEffectName() => effect?.effectName ?? "None";
    public float GetCooldown() => effect?.cooldown ?? 0f;
}
