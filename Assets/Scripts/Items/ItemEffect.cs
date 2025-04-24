using UnityEngine;

public abstract class ItemEffect : ScriptableObject, IItemEffect
{
    public abstract void Apply(Character character);
    public abstract void Remove(Character character);
}
