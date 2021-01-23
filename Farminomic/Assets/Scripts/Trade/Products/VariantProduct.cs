using System;
using UnityEngine;

[Serializable]
public class VariantProduct<T> : AbstractProduct
{
    [SerializeField] private T prodName;

    public T ProdName => prodName;
    public override string Name => ProdName.ToString();

    protected VariantProduct(int currentCount)
    {
        this.currentCount = currentCount;
    }

    protected VariantProduct(T prodName, Sprite icon, int price)
    {
        this.prodName = prodName;
        this.icon = icon;
        examplePrice = price;
    }

    public override IExchangable Get(int count)
    {
        VariantProduct<T> prod = new VariantProduct<T>(count);
        prod.icon = Icon;
        prod.prodName = ProdName;

        return prod;
    }
}
