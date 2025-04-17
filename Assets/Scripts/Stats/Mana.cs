using System;

[Serializable]
public class Mana : Resource
{
    public override float Current
    {
        get => base.Current;

        set
        {
            base.Current = value;

            if (value > current)
            {
                OnRestore?.Invoke(value - current);
            }
            else
            {
                OnSpend?.Invoke(current - value);
            }
        }
    }
    public ManaRegeneration regeneration;

    public event Action<float> OnSpend;
    public event Action<float> OnRestore;
    public Mana() : base() { }
    public virtual void Spend(float amount)
    {
        current -= amount;

        if (current < 0)
        {
            current = 0;
        }

        OnSpend?.Invoke(amount);
    }
    public virtual void Restore(float amount)
    {
        current += amount;

        if (current > total)
        {
            current = total;
        }

        OnRestore?.Invoke(amount);
    }

}
