using System;
using UnityEngine;

[Serializable]
public class Product
{
    public Quality Quality;
    public ProductType Type;
    public string EnumName;
    protected int numberInList;

    [Header("Информации о продукте")]
    [SerializeField] protected MenuShowItem menuShowItem;

    public int NumberInList { get => numberInList; set => numberInList = value; }

    public MenuShowItem GetMenuShowItem()
    {
        return menuShowItem;
    }

    public override bool Equals(object obj)
    {
        return obj is Product product &&
               Quality == product.Quality &&
               Type == product.Type &&
               numberInList == product.numberInList;
    }

    public override int GetHashCode()
    {
        int hashCode = -1745646078;
        hashCode = hashCode * -1521134295 + Quality.GetHashCode();
        hashCode = hashCode * -1521134295 + Type.GetHashCode();
        hashCode = hashCode * -1521134295 + numberInList.GetHashCode();
        return hashCode;
    }
}
