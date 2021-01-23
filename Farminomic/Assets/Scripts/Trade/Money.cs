
using System;

[Serializable]
public class Money : AbstractProduct
{
    private Money(int count)
    {
        currentCount = count;
    }

    //todo позже название брать либо из xml с локализацией либо из БД
    public override string Name => "Деньги";

    public override IExchangable Get(int count)
    {
        Spend(count);
        return new Money(count);
    }
}
