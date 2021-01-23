using System;
using UnityEngine;

[Serializable]
public class EatableProduct : VariantProduct<EatableProdName>
{
    protected EatableProduct(int currentCount) : base(currentCount) { }

    public EatableProduct(EatableProdName prodName, Sprite icon, int price) : base(prodName, icon, price) { }

}
