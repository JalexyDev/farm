using System;
using UnityEngine;

[Serializable]
public abstract class AbstractProduct : IExchangable, IValuable
{
    //todo убрать лишние SerializeField
    [SerializeField] protected Sprite icon;
    [SerializeField] protected int examplePrice;
    [SerializeField] protected int currentCount;

    public Sprite Icon { get => icon; }
    public int Price { get => examplePrice; set => examplePrice = value; }
    public int Count => currentCount;
    public abstract string Name { get; }

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

    public abstract IExchangable Get(int count);

    public bool IsEnoughForExchange(int count)
    {
        return currentCount >= count;
    }
}
