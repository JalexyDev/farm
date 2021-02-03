using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Placer placableController;
    private SelectedMenu menu;

    public void ShowMenu(MenuShowItem item)
    {
        GetMenu().ShowMenu(item);
    }

    public void ShowRootMenu(MenuShowItem item)
    {
        GetMenu().ShowRootMenu(item);
    }

    public void CloseMenu()
    {
        GetMenu().CloseMenu();
    }

    public void CloseRootMenu()
    {
        GetMenu().CloseRootMenu();
    }

    public void ShowPrice(ProductItemsList products)
    {
        GetMenu().ShowPrice(products);
    }

    public void StartPlacing(GroundPlacablePreview placablePrefab)
    {
        GetPlacer().StartPlacing(placablePrefab);
    }

    public bool IsResourcesEnough(ProductItemsList products)
    {
        return GetPlacer().IsResourcesEnough(products);
    }

    public void SpendRecources(ProductItemsList products)
    {
        GetPlacer().SpendRecources(products);
    }

    public void AddRecources(ProductItemsList products)
    {
        GetPlacer().AddRecources(products);
    }

    public Camera GetMainCamera()
    {
        return GetPlacer().GetMainCamera();
    }

    private SelectedMenu GetMenu()
    {
        if (menu == null)
        {
            menu = GameObject.FindGameObjectWithTag("SelectedMenu").GetComponent<SelectedMenu>();
        }

        return menu;
    }

    private Placer GetPlacer()
    {
        if (placableController == null)
        {
            placableController = GetComponent<Placer>();
        }

        return placableController;
    }
}
