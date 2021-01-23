
using UnityEngine;

public interface IExchangable
{
    string Name { get; }
    Sprite Icon { get; }
    int Count { get; }

    bool IsEnoughForExchange(int count);
    IExchangable Get(int count);
    int Add(int count);
    int Spend(int count);
}
    

