using System.Collections.Generic;
using UnityEngine;

public class StockController : MonoBehaviour
{
    public static StockController Instance;

    [SerializeField] private ProductItemsList productItemsList;

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

        DontDestroyOnLoad(gameObject);

        InitStock();
    }

    //todo считываем данные из БД
    private void InitStock()
    {
        
    }

    public void AddRecources(ProductItemsList products)
    {
        var currentList = GetProductItemsList();
        productItemsList = currentList + products;
    }

    public bool SpendRecources(ProductItemsList products)
    {
        if (IsResourcesEnough(products))
        {
            productItemsList = GetStockDifference(products);
            return true;
        }

        return false;
    }

    public bool IsResourcesEnough(ProductItemsList products)
    {
        ProductItemsList difference = GetStockDifference(products);

        foreach (ProductItem item in difference.ProductItems)
        {
            if (item.Count < 0)
            {
                return false;
            }
        }

        return true;
    }

    public ProductItemsList GetStockDifference(ProductItemsList products)
    {
        return GetProductItemsList() - products;
    }

    public ProductItemsList GetProductItemsList()
    {
        if (productItemsList == null)
        {
            productItemsList = new ProductItemsList();
        }

        return productItemsList;
    }
}
