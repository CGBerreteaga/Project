using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stats : Wrapper
{
    List<IModifier> modifiers;
    List<IModifier> Modifiers
    {
        get
        {
            return modifiers;
        }
    }

    public Action<float> onChange;

    public float Total
    {
        get
        {
            var total = amount;
            foreach (var modifier in Modifiers)
            {
                total += modifier.Amount;
            }
            return total;
        }
    }

    public Stats(float amount)
    {
        this.amount = amount;
        modifiers = new List<IModifier>();
        onChange?.Invoke(Total);
    }

    public void Upgrade()
    {
        amount++;
        onChange?.Invoke(Total);
    }

    public void AddModifier(IModifier modifier)
    {
        Modifiers.Add(modifier);
        onChange?.Invoke(Total);
    }

    public void RemoveModifier(IModifier modifier)
    {
        Modifiers.Remove(modifier);
        onChange?.Invoke(Total);
    }

    public override string ToString()
    {
        return attribute?.ToString();
    }
}
