using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    [TextArea(5, 10)] public string itemDescription;
    public Sprite icon;

    public abstract void OnEquip(Character owner);
    public abstract void OnUnequip(Character owner);
}
