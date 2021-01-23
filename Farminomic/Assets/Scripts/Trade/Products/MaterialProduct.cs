using System;
using UnityEngine;

[Serializable]
public class MaterialProduct : VariantProduct<MaterialProdName>
{
    protected MaterialProduct(int currentCount) : base(currentCount) { }

    public MaterialProduct(MaterialProdName prodName, Sprite icon, int price) : base(prodName, icon, price) { }

}
