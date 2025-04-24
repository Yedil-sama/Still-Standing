using UnityEngine;

public abstract class ItemActiveEffect : ScriptableObject
{
    public string effectName;
    public float cooldown;
    protected float lastUseTime = -Mathf.Infinity;

    public virtual bool CanActivate(Character owner) => Time.time >= lastUseTime + cooldown;

    public void TryActivate(Character owner, Character target)
    {
        if (CanActivate(owner))
        {
            lastUseTime = Time.time;
            Activate(owner, target);
        }
    }

    protected abstract void Activate(Character owner, Character target);
}
