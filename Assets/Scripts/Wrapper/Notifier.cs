using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Notifier: Wrapper
{
    [SerializeField] float max;
    public float Max => max;
    public event Action onChange;
    public Notifier(float amount, float max)
    {
        this.amount = amount;
        this.max = max;
    }

    public void Add(float value)
    {
        amount += value;
        amount = Mathf.Clamp(amount, 0, max);
        onChange?.Invoke();
    }
}
