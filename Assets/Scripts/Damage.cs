using System;

public enum DamageType
{
    Physical,
    Magical,
    True
}

[Serializable]
public struct Damage
{
    public float amount;
    public DamageType type;
    public Damage(float amount) : this(amount, DamageType.Physical) { }
    public Damage(float amount, DamageType type)
    {
        this.amount = amount;
        this.type = type;
    }
}
