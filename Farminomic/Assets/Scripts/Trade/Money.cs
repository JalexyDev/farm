using UnityEngine;
using System;

[Serializable]
public class Money : Product
{
    [SerializeField] private int currentCount;

    public int Count { get => currentCount; set => currentCount = value; }

    public int Add(int count)
    {
        currentCount += count;
        return currentCount;
    }

    public int Spend(int count)
    {
        currentCount -= count;
        return currentCount;
    }

    public bool IsEnoughForExchange(int count)
    {
        return currentCount >= count;
    }
}
