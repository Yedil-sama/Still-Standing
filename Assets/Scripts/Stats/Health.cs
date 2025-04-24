using System;

[Serializable]
public class Health : Resource
{
    public override float Current 
    {
        get => base.Current;

        set
        {
            base.Current = value;

            if (value > current)
            {
                OnHealUp?.Invoke(value - current);
            }
            else
            {
                OnTakeDamage?.Invoke(current - value);
            }

            ClampCurrent();
        }
    }
    public bool isDead => current <= 0;
    public HealthRegeneration regeneration;

    public event Action<float> OnTakeDamage;
    public event Action<float> OnHealUp;
    public float TakeDamage(float amount)
    {
        if (isDead) return 0;
        if (amount <= 0) return 0;

        current -= amount;

        if (current <= 0)
        {
            amount += current;
        }

        OnTakeDamage?.Invoke(amount);
        return amount;
    }
    public float HealUp(float amount)
    {
        if (isDead) return 0;
        if (amount <= 0) return 0;

        current += amount;

        if (current > total)
        {
            (current, amount) = (total, amount + total - current);

        }

        OnHealUp?.Invoke(amount);
        return amount;
    }

}
