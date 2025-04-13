using System;
using UnityEngine;

[Serializable]
public class Stat : IStat
{
    [SerializeField] protected float current;
    public virtual float Current
    {
        get => current;
        set
        {
            current = value;
            ClampCurrent();
        }
    }

    [SerializeField] protected float baseValue;
    public virtual float BaseValue
    {
        get => baseValue;
        set
        {
            baseValue = value;
            ClampCurrent();
        }
    }

    [SerializeField] protected float bonus;
    public virtual float Bonus
    {
        get => bonus;
        set
        {
            bonus = value;
            total = baseValue + bonus;
            ClampCurrent();
        }
    }

    [SerializeField] protected float total;
    public virtual float Total
    {
        get => total = baseValue + bonus;
        set
        {
            float difference = total - value;

            if (bonus >= difference)
            {
                bonus -= difference;
            }
            else
            {
                float remaining = difference - bonus;

                bonus = 0;
                baseValue -= remaining;

                if (baseValue < 0)
                {
                    baseValue = 0;
                }
            }
            ClampCurrent();
        }
    }


    public void Start()
    {
        Current = Total;
    }

    public virtual void OnValidate()
    {
        ClampCurrent();
    }

    public void ClampCurrent()
    {
        if (Current > Total)
        {
            Current = Total;
        }
        if (Current < 0)
        {
            Current = 0;
        }
    }
}
