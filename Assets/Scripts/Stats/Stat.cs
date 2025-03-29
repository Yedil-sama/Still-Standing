using System;

[Serializable]
public class Stat
{
    //public string name;
    public float current;
    public float baseValue;
    public float bonus;
    public float total => baseValue + bonus;

    public void Start()
    {
        //name = GetType().Name;
        current = total;
    }

    public virtual void AddBonus(float amount)
    {
        if (amount < 0) return;
        bonus += amount;
        ClampCurrent();
    }

    public virtual void RemoveBonus(float amount)
    {
        if (amount < 0) return;
        bonus -= amount;
        ClampCurrent();
    }

    public virtual void OnValidate()
    {
        ClampCurrent();
    }
    private void ClampCurrent()
    {
        if (current > total)
        {
            current = total;
        }
        if (current < 0)
        {
            current = 0;
        }
    }
}
