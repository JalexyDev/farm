using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Todo добавлением возможностей игроку связанных со зданием
public class Building : MonoBehaviour, IShowable, IClickable
{
    // Отдельный префаб, задается в инспекторе
    public BuildingPreview Preview;
    
    [Header("Информации о здании")]
    public MenuShowItem menuShowItem;

    // Компонент этого объекта. Отвечает за хранение позиции и перемещение
    private BuildingPlacable Placable;

    private BuildingController controller;
   
    private void Awake()
    {
        GetPlacable();
        InitFunctions();
        InitControlBtns();
    }

    public MenuShowItem GetMenuShowItem()
    {
        return menuShowItem;
    }

    public void OnClick()
    {
        if (Controls.OpenMenuDisabled)
        {
            return;
        }

        GetBuildingController().ShowRootMenu(menuShowItem);
    }

    public BuildingPreview InstPreviewHere(Vector3 pose)
    {
        BuildingPreview prevInstance = Instantiate(Preview, pose, Quaternion.identity);

        prevInstance.PlacablePrice = GetPrice();
        prevInstance.IsRotated = GetPlacable().IsRotated();

        prevInstance.LastPlacablePlace = GetPlacable().GridPlace;

        prevInstance.PlacableFunc = (isRotated, gridPose) =>
        {
            BuildingPlacable placable;
            if (prevInstance.IsJustShifting())
            {
                placable = GetPlacable();
                placable.transform.position = prevInstance.transform.position;
            }
            else
            {
                placable = Instantiate(GetPlacable(), prevInstance.transform.position, Quaternion.identity);
            }

            placable.GridPlace = gridPose;
            placable.SetRotation(isRotated);

            return placable;
        };

        return prevInstance;
    }

    protected virtual void InitFunctions()
    {
        // Если здание обладает дополнительными функциями, надо переопределить этот метод
        //menuShowItem.AddFunctionList(fun1, fun2, ...);
    }

    // Если у здания есть дополнительные кнопки контроля, добавить их, переопределив это
    protected virtual void InitControlBtns()
    {
        //todo определять accessibility по статусу (типа если нет подписки, перемещать нельзя но видно заблоченую кнопку)

        ControlBtn move = new ControlBtn(ControlPanelPart.MOVE, true, () =>
        {
            GetPlacable().StartShifting(InstPreviewHere(transform.position), GetBuildingController());
        });

        ControlBtn delete = new ControlBtn(ControlPanelPart.CANCEL, true, () =>
        {
            //todo снести здание

        });

        menuShowItem.AddControlBtnList(move, delete);

    }

    public BuildingPlacable GetPlacable()
    {
        if (Placable == null)
        {
            Placable = GetComponent<BuildingPlacable>();
        }

        return Placable;
    }

    public ProductItemsList GetPrice()
    {
        if (menuShowItem.Price == null)
        {
            menuShowItem.Price = new ProductItemsList();
        }

        return menuShowItem.Price;
    }

    protected BuildingController GetBuildingController()
    {
        if (controller == null)
        {
            controller = BuildingController.Instance;
        }

        return controller;
    }
}
