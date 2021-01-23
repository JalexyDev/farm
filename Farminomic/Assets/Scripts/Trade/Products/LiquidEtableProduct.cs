using System;
using UnityEngine;

[Serializable]
public class LiquidEtableProduct : VariantProduct<LiquidEatableProdName>
{
    protected LiquidEtableProduct(int currentCount) : base(currentCount) { }

    public LiquidEtableProduct(LiquidEatableProdName prodName, Sprite icon, int price) : base(prodName, icon, price) { }
}
