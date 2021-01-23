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

    public void ShowPrice(int price)
    {
        GetMenu().ShowPrice(price);
    }

    public void StartPlacing(GroundPlacablePreview placablePrefab)
    {
        GetPlacer().StartPlacing(placablePrefab);
    }

    public bool IsEnoughMoney(int price)
    {
        return GetPlacer().IsEnoughMoney(price);
    }

    public void SpendMoney(int count)
    {
        GetPlacer().SpendMoney(count);
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
