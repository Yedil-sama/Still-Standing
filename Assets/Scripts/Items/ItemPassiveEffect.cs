using UnityEngine;

public class ItemPassiveEffect : ScriptableObject
{
    public string effectName;
    public virtual void OnEquip(Character owner) { }
    public virtual void OnUnequip(Character owner) { }
    public virtual void OnAttack(Character owner, Character target) { }
    public virtual void OnHit(Character owner, Character target, Damage damageDealt) { }
    public virtual void OnTakeDamage(Character owner, float amount) { }
    public virtual void Tick(Character owner, float deltaTime) { }
}
