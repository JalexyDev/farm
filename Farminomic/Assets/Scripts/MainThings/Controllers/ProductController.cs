using System.Collections.Generic;
using UnityEngine;

//todo класс хранящий списки всех продуктов и материалов в игре и позволяющий получать их по отдельности и списком
//todo для удобства заполнения сделать кастомный инспектор и кнопку сортировки сортировки по имени для каждого списка

public class ProductController : MonoBehaviour
{
    public static ProductController Instance;

    public List<Product> Products;


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

    //todo добавить получение списка имен продуктов или их номеров с использованием фильтра по ProductType

    public List<Product> GetProdListSortedByName(List<Product> list)
    {
        var copyList = new List<Product>(list);
        copyList.Sort(delegate (Product prod1, Product prod2)
        {
            return CompareNames(prod1.GetMenuShowItem().Information.Name, prod2.GetMenuShowItem().Information.Name);
        });

        return copyList;
    }

    public List<Product> GetProdListSortedByQuality<T>(List<Product> list)
    {
        var copyList = new List<Product>(list);
        copyList.Sort(delegate (Product prod1, Product prod2)
        {
            int qualityDif = ((int)prod1.Quality).CompareTo((int)prod2.Quality);
            if (qualityDif == 0)
            {
                return CompareNames(prod1.GetMenuShowItem().Information.Name, prod2.GetMenuShowItem().Information.Name);
            }

            return qualityDif;

        });

        return copyList;
    }

    private int CompareNames(string name1, string name2)
    {
        if (name1 == null && name2 == null) return 0;
        else if (name1 == null) return -1;
        else if (name2 == null) return 1;
        else return name1.CompareTo(name2);
    }

    public Product GetProduct(ProductNames name)
    {
        int nameIndex = (int)name;
        if (Products == null || Products.Count == 0 || nameIndex < 0 || nameIndex > Products.Count)
        {
            return null;
        }

        if (Products[nameIndex].NumberInList != nameIndex)
        {
            Products[nameIndex].NumberInList = nameIndex;
        }

        return Products[nameIndex];
    }
}


