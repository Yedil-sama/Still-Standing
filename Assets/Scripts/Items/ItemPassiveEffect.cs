using UnityEngine;

public abstract class ItemPassiveEffect : ScriptableObject
{
    public string effectName;

    public virtual void OnEquip(Character owner)
    {
        Subscribe(owner);
    }

    public virtual void OnUnequip(Character owner)
    {
        Unsubscribe(owner);
    }

    protected virtual void Subscribe(Character owner)
    {
        owner.OnTakeDamage += amount => OnTakeDamage(owner, amount);

        if (owner.autoAttack != null)
        {
            owner.autoAttack.OnAutoAttackPerformed += target =>
            {
                OnAttack(owner, target);
            };
        }

    }

    protected virtual void Unsubscribe(Character owner) { }

    public virtual void OnAttack(Character owner, Character target) { }
    public virtual void OnHit(Character owner, Character target, Damage damageDealt) { }
    public virtual void OnCast(Character owner, Ability spell, Character target = null) { }
    public virtual void OnTakeDamage(Character owner, float amount) { }
    public virtual void Tick(Character owner, float deltaTime) { }
}
