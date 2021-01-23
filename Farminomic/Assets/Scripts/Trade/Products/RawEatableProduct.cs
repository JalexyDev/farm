
using System;
using UnityEngine;

[Serializable]
public class RawEatableProduct : VariantProduct<RawEatableProdName>
{
    protected RawEatableProduct(int currentCount) : base(currentCount) { }
    public RawEatableProduct(RawEatableProdName prodName, Sprite icon, int price) : base(prodName, icon, price) { }
}
