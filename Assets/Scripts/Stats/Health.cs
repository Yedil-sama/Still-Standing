using System;

[Serializable]
public class Health : Stat
{
    public bool isDead => current <= 0;
    public Health() : base() { }
    public float TakeDamage(float amount)
    {
        if (isDead) return 0;
        if (amount <= 0) return 0;

        current -= amount;

        if (current <= 0)
        {
            amount += current;
        }

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

        return amount;
    }

}
