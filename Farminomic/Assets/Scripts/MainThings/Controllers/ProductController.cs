using System;
using System.Collections.Generic;
using UnityEngine;

//todo класс хранящий списки всех продуктов и материалов в игре и позволяющий получать их по отдельности и списком
//todo для удобства заполнения сделать кастомный инспектор и кнопку сортировки сортировки по имени для каждого списка

public class ProductController : MonoBehaviour
{
    public static ProductController Instance;

    [SerializeField] private List<RawEatableProduct> rawEatableProducts;
    [SerializeField] private List<RawMaterialProduct> rawMaterialProducts;
    [SerializeField] private List<EatableProduct> eatableProducts;
    [SerializeField] private List<LiquidEtableProduct> liquidEtableProducts;
    [SerializeField] private List<MaterialProduct> materialProducts;

    public List<RawEatableProduct> RawEatableProducts { get => rawEatableProducts; set => rawEatableProducts = value; }
    public List<RawMaterialProduct> RawMaterialProducts { get => rawMaterialProducts; set => rawMaterialProducts = value; }
    public List<EatableProduct> EatableProducts { get => eatableProducts; set => eatableProducts = value; }
    public List<LiquidEtableProduct> LiquidEtableProducts { get => liquidEtableProducts; set => liquidEtableProducts = value; }
    public List<MaterialProduct> MaterialProducts { get => materialProducts; set => materialProducts = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        if (Application.isPlaying)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public List<T> GetProdListSortedByName<T>(List<T> list) where T : AbstractProduct
    {
        var copyList = new List<T>(list);
        copyList.Sort(delegate (T prod1, T prod2)
        {
            if (prod1.Name == null && prod2.Name == null) return 0;
            else if (prod1.Name == null) return -1;
            else if (prod2.Name == null) return 1;
            else return prod1.Name.CompareTo(prod2.Name);
        });

        return copyList;

    }

    public List<T> GetProdListSortedByPrice<T>(List<T> list) where T : AbstractProduct
    {
        var copyList = new List<T>(list);
        copyList.Sort(delegate (T prod1, T prod2)
        {
            return prod1.Price.CompareTo(prod2.Price);
        });

        return copyList;

    }

    public RawEatableProduct GetRawEatableProduct(RawEatableProdName name)
    {
        return RawEatableProducts[(int) name];
    }

    public RawMaterialProduct GetRawMaterialProduct(RawMaterialProdName name)
    {
        return RawMaterialProducts[(int)name];
    }

    public EatableProduct GetEatableProduct(EatableProdName name)
    {
        return EatableProducts[(int)name];
    }

    public MaterialProduct GetMaterialProduct(MaterialProdName name)
    {
        return MaterialProducts[(int)name];
    }

    public LiquidEtableProduct GetLiquidEtableProduct(LiquidEatableProdName name)
    {
        return LiquidEtableProducts[(int)name];
    }
}


