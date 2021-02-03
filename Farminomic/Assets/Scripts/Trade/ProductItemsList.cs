using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProductItemsList
{
    [SerializeField] private List<ProductItem> productItems;

    public List<ProductItem> ProductItems {
        get
        {
            if (productItems == null)
            {
                productItems = new List<ProductItem>();
            }
            return productItems;
        }
        set => productItems = value;
    }

    public ProductItemsList()
    {
        productItems = new List<ProductItem>();
    }

    public ProductItemsList(List<ProductItem> productItems)
    {
        this.productItems = productItems;
    }


    public ProductItem GetItemLikeThis(ProductItem item)
    {
        if (ProductItems.Count > 0 && ProductItems.Contains(item))
        {
            return ProductItems[ProductItems.IndexOf(item)];
        }

        return null;
    }

    public static ProductItemsList operator +(ProductItemsList p1, ProductItemsList p2)
    {
        if (p1 == null && p2 != null)
        {
            return p2;
        } 
        else if (p2 == null && p1 != null)
        {
            return p1;
        }
        else if (p1 == null && p2 == null)
        {
            return null;
        }

        List<ProductItem> productItems = new List<ProductItem>();

        ProductItem anotherItem;
        foreach (ProductItem item in p1.ProductItems)
        {
            anotherItem = p2.GetItemLikeThis(item);

            if (anotherItem != null)
            {
                productItems.Add(item + anotherItem);
            }
            else
            {
                productItems.Add(item);
            }
        }

        foreach (ProductItem item in p2.ProductItems)
        {
            if (productItems.Contains(item)) { continue; }

            productItems.Add(item);
        }

        return new ProductItemsList(productItems);
    }

    public static ProductItemsList operator -(ProductItemsList p)
    {
        List<ProductItem> productItems = new List<ProductItem>();

        foreach (ProductItem item in p.ProductItems)
        {
            productItems.Add(-item);
        }

        return new ProductItemsList(productItems);
    }

    public static ProductItemsList operator -(ProductItemsList p1, ProductItemsList p2)
    {
        if (p1 == null && p2 != null)
        {
            return -p2;
        }
        else if (p2 == null && p1 != null)
        {
            return p1;
        }
        else if (p1 == null && p2 == null)
        {
            return null;
        }

        if (p2 == null || p2.ProductItems == null || p2.ProductItems.Count == 0)
        {
            return p1;
        }

        List<ProductItem> productItems = new List<ProductItem>();

        ProductItem anotherItem;
        foreach (ProductItem item in p1.ProductItems)
        {
            anotherItem = p2.GetItemLikeThis(item);

            if (anotherItem != null)
            {
                productItems.Add(item - anotherItem);
            }
            else
            {
                productItems.Add(item);
            }
        }

        foreach (ProductItem item in p2.ProductItems)
        {
            if (productItems.Contains(item)) { continue; }

            productItems.Add(-item);
        }

        return new ProductItemsList(productItems);
    }

    public static ProductItemsList operator *(ProductItemsList p1, float multiplier)
    {
        List<ProductItem> productItems = new List<ProductItem>();

        foreach (ProductItem item in p1.ProductItems)
        {
            productItems.Add(item * multiplier);
        }

        return new ProductItemsList(productItems);
    }
}
