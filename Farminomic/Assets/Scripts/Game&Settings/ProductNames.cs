using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ProductNames : MonoBehaviour
{
    public ProductController productController = ProductController.Instance;
    public List<ProductParams> rawEatableProducts;
    public List<ProductParams> rawMaterialProducts;
    public List<ProductParams> eatableProducts;
    public List<ProductParams> liquidEtableProducts;
    public List<ProductParams> materialProducts;

    private void Awake()
    {
        rawEatableProducts.Clear();
        foreach (RawEatableProduct product in GetProductController().RawEatableProducts)
        {
            rawEatableProducts.Add(new ProductParams(product.Name.ToString(), product.Icon, product.Price));
        }

        rawMaterialProducts.Clear();
        foreach (RawMaterialProduct product in GetProductController().RawMaterialProducts)
        {
            rawMaterialProducts.Add(new ProductParams(product.Name.ToString(), product.Icon, product.Price));
        }

        eatableProducts.Clear();
        foreach (EatableProduct product in GetProductController().EatableProducts)
        {
            eatableProducts.Add(new ProductParams(product.Name.ToString(), product.Icon, product.Price));
        }

        materialProducts.Clear();
        foreach (MaterialProduct product in GetProductController().MaterialProducts)
        {
            materialProducts.Add(new ProductParams(product.Name.ToString(), product.Icon, product.Price));
        }

        liquidEtableProducts.Clear();
        foreach (LiquidEtableProduct product in GetProductController().LiquidEtableProducts)
        {
            liquidEtableProducts.Add(new ProductParams(product.Name.ToString(), product.Icon, product.Price));
        }
    }

    public void SaveAllProducts()
    {
        SaveRawEatable();
        SaveRawMaterials();
        SaveEatables();
        SaveMaterials();
        SaveLiquids();
    }

    private void SaveRawEatable()
    {
        var rawEatebleNames = GetValues<RawEatableProdName>();
        var currentRawEatableList = GetProductController().RawEatableProducts;

        for (int i = 0; i < rawEatebleNames.Count(); i++)
        {
            if (i < currentRawEatableList.Count())
            {
                currentRawEatableList.RemoveAt(i);
                currentRawEatableList.Insert(i, new RawEatableProduct(rawEatebleNames[i], rawEatableProducts[i].Icon, rawEatableProducts[i].ExamplePrice));
            }
            else
            {
                currentRawEatableList.Add(new RawEatableProduct(rawEatebleNames[i], rawEatableProducts[i].Icon, rawEatableProducts[i].ExamplePrice));
            }
        }

        if (currentRawEatableList.Count > rawEatebleNames.Count)
        {
            currentRawEatableList.RemoveRange(rawEatebleNames.Count, currentRawEatableList.Count - rawEatebleNames.Count);
        }

        GetProductController().RawEatableProducts = currentRawEatableList;
    }

    private void SaveRawMaterials()
    {
        var rawMaterialNames = GetValues<RawMaterialProdName>();
        var currentRawMaterialsList = GetProductController().RawMaterialProducts;

        for (int i = 0; i < rawMaterialNames.Count(); i++)
        {
            if (i < currentRawMaterialsList.Count())
            {
                currentRawMaterialsList.RemoveAt(i);
                currentRawMaterialsList.Insert(i, new RawMaterialProduct(rawMaterialNames[i], rawMaterialProducts[i].Icon, rawMaterialProducts[i].ExamplePrice));
            }
            else
            {
                currentRawMaterialsList.Add(new RawMaterialProduct(rawMaterialNames[i], rawMaterialProducts[i].Icon, rawMaterialProducts[i].ExamplePrice));
            }
        }

        if (currentRawMaterialsList.Count > rawMaterialNames.Count)
        {
            currentRawMaterialsList.RemoveRange(rawMaterialNames.Count, currentRawMaterialsList.Count - rawMaterialNames.Count);
        }

        GetProductController().RawMaterialProducts = currentRawMaterialsList;
    }

    private void SaveEatables()
    {
        var eatableNames = GetValues<EatableProdName>();
        var currentEatableList = GetProductController().EatableProducts;

        for (int i = 0; i < eatableNames.Count(); i++)
        {
            if (i < currentEatableList.Count())
            {
                currentEatableList.RemoveAt(i);
                currentEatableList.Insert(i, new EatableProduct(eatableNames[i], eatableProducts[i].Icon, eatableProducts[i].ExamplePrice));
            }
            else
            {
                currentEatableList.Add(new EatableProduct(eatableNames[i], eatableProducts[i].Icon, eatableProducts[i].ExamplePrice));
            }
        }

        if (currentEatableList.Count > eatableNames.Count)
        {
            currentEatableList.RemoveRange(eatableNames.Count, currentEatableList.Count - eatableNames.Count);
        }

        GetProductController().EatableProducts = currentEatableList;
    }

    private void SaveMaterials()
    {
        var materialNames = GetValues<MaterialProdName>();
        var currentMaterialList = GetProductController().MaterialProducts;

        for (int i = 0; i < materialNames.Count(); i++)
        {
            if (i < currentMaterialList.Count())
            {
                currentMaterialList.RemoveAt(i);
                currentMaterialList.Insert(i, new MaterialProduct(materialNames[i], materialProducts[i].Icon, materialProducts[i].ExamplePrice));
            }
            else
            {
                currentMaterialList.Add(new MaterialProduct(materialNames[i], materialProducts[i].Icon, materialProducts[i].ExamplePrice));
            }
        }

        if (currentMaterialList.Count > materialNames.Count)
        {
            currentMaterialList.RemoveRange(materialNames.Count, currentMaterialList.Count - materialNames.Count);
        }

        GetProductController().MaterialProducts = currentMaterialList;
    }

    private void SaveLiquids()
    {
        var liquidEatableNames = GetValues<LiquidEatableProdName>();
        var currentLiquidEatableList = GetProductController().LiquidEtableProducts;

        for (int i = 0; i < liquidEatableNames.Count(); i++)
        {
            if (i < currentLiquidEatableList.Count())
            {
                currentLiquidEatableList.RemoveAt(i);
                currentLiquidEatableList.Insert(i, new LiquidEtableProduct(liquidEatableNames[i], liquidEtableProducts[i].Icon, liquidEtableProducts[i].ExamplePrice));
            }
            else
            {
                currentLiquidEatableList.Add(new LiquidEtableProduct(liquidEatableNames[i], liquidEtableProducts[i].Icon, liquidEtableProducts[i].ExamplePrice));
            }
        }

        if (currentLiquidEatableList.Count > liquidEatableNames.Count)
        {
            currentLiquidEatableList.RemoveRange(liquidEatableNames.Count, currentLiquidEatableList.Count - liquidEatableNames.Count);
        }

        GetProductController().LiquidEtableProducts = currentLiquidEatableList;
    }

    private List<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }

    private ProductController GetProductController()
    {
        if (productController == null)
        {
            productController = FindObjectOfType<ProductController>();
        }

        return productController;
    }
}

[Serializable]
public class ProductParams
{
    public string Name;
    public Sprite Icon;
    public int ExamplePrice;

    public ProductParams(string name, Sprite icon, int examplePrice)
    {
        Name = name;
        Icon = icon;
        ExamplePrice = examplePrice;
    }

    public override string ToString()
    {
        return Name;
    }
}

