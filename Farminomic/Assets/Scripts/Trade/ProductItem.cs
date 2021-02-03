using System;
using UnityEngine;

[Serializable]
public class ProductItem
{
    public ProductNames Name;
    public int Count;

    public Product Product;

    private ProductController productController;

    private ProductItem(ProductItem item, int count)
    {
        Name = item.Name;
        Product = item.Product;
        Count = count;
    }

    public Product GetProduct()
    {
        if (Product == null || (int)Name != Product.NumberInList)
        {
            Product = GetProductController().GetProduct(Name);
        }

        return Product;
    }

    public ProductController GetProductController()
    {
        if (productController == null)
        {
            productController = ProductController.Instance;
        }

        return productController;
    }

    public static ProductItem operator +(ProductItem p1, ProductItem p2)
    {
        if (p1.Name != p2.Name)
        {
            throw new ArgumentException(p1.GetProduct().ToString() + " != " + p2.GetProduct().ToString());
        }

        return new ProductItem(p1, p1.Count + p2.Count);
    }

    public static ProductItem operator +(ProductItem p1, int count)
    {
        return new ProductItem(p1, p1.Count + count);
    }

    public static ProductItem operator -(ProductItem p1) => new ProductItem(p1, -p1.Count);

    public static ProductItem operator -(ProductItem p1, ProductItem p2)
    {
        if (p1.Name != p2.Name)
        {
            throw new ArgumentException(p1.GetProduct().ToString() + " != " + p2.GetProduct().ToString());
        }

        return new ProductItem(p1, p1.Count - p2.Count);
    }

    public static ProductItem operator -(ProductItem p1, int count)
    {
        return new ProductItem(p1, p1.Count - count);
    }

    public static ProductItem operator *(ProductItem p1, float count)
    {
        return new ProductItem(p1, Mathf.FloorToInt(p1.Count * count));
    }

    public static ProductItem operator /(ProductItem p1, int count)
    {
        return new ProductItem(p1, p1.Count / count);
    }

    public override bool Equals(object obj)
    {
        return obj is ProductItem item && GetProduct().NumberInList == item.GetProduct().NumberInList;
    }

    public override int GetHashCode()
    {
        int hashCode = 1855322335;
        hashCode = hashCode * -1521134295 + Product.NumberInList.GetHashCode();
        return hashCode;
    }
}

