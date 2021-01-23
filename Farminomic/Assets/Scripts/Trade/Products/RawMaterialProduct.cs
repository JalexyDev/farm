using System;
using UnityEngine;

[Serializable]
public class RawMaterialProduct : VariantProduct<RawMaterialProdName>
{
    protected RawMaterialProduct(int currentCount) : base(currentCount) { }
    public RawMaterialProduct(RawMaterialProdName prodName, Sprite icon, int price) : base(prodName, icon, price) { }
}
