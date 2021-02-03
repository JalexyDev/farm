using System.Collections.Generic;
using UnityEngine;

//todo отвечает за строительство здания на месте превьюшки
// перенос здания из одного места в другое
// денежные затраты на строительство и компенсацию при сносе

public class Placer : MonoBehaviour
{
    public List<GroundPlacable> placedThings;

    private StockController stock;
    private TileMapHolder grid;
    private GroundPlacablePreview placablePreview;
    private Camera mainCamera;
    private SelectedMenu menu;

    private List<ControlBtn> controlBtns;

    private void Awake()
    {
        Init();  
    }

    protected void Init()
    {
        //todo инициализировать из БД и сообщить гриду занятые клетки
        placedThings = new List<GroundPlacable>();
        stock = StockController.Instance;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (placablePreview != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacing();
                return;
            }

            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = GetGrid().GetGridPosHere(mouse);
            
            Vector2 cellCenter;
            if (GetGrid().IsAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))
            {
                cellCenter = GetGrid().GetGridCellPosition(gridPos);
            }
            else
            {
                cellCenter = mouse;
            }

            placablePreview.SetCurrentMousePosition(cellCenter, gridPos, () => GetGrid().IsBuildAvailable(gridPos, placablePreview));
        }
    }

    public void BuildMainHouse(Building building)
    {
        StartPlacing(building.InstPreviewHere(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0)));
    }

    private void ShowPlacingMenu()
    {
        if (placablePreview != null)
        {
            ProductItemsList products = placablePreview.IsJustShifting() ? null : placablePreview.PlacablePrice;

            GetMenu().ShowMenu(new MenuShowItem(placablePreview.PlacableInfo, null, GetControllBtns(), products));
        }
    }

    private void HidePlacingMenu()
    {
        GetMenu().CloseMenu();
    }

    //Вызывается кнопками на интерфейсе
    public void StartPlacing(GroundPlacablePreview placablePrefab)
    {
        if (placablePrefab.IsJustShifting())
        {
            FreeCells(placablePrefab.LastPlacablePlace);
            ShowPlacablePreview(placablePrefab);
        }
        else
        {
            if (IsResourcesEnough(placablePrefab.PlacablePrice))
            {
                ShowPlacablePreview(placablePrefab);
            }
        }
    }

    private void ShowPlacablePreview(GroundPlacablePreview preview)
    {
        if (placablePreview != null)
        {
            Destroy(placablePreview.gameObject);
        }

        placablePreview = preview;
       
        Vector2Int gridPos = GetGrid().GetGridPosHere(placablePreview.transform.position);

        if (GetGrid().IsAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))
        {
            placablePreview.SetSpawnPosition(gridPos);
            placablePreview.SetBuildAvailable(GetGrid().IsBuildAvailable(gridPos, placablePreview));
        }
        else
        {
            placablePreview.SetBuildAvailable(false);
        }

        ShowPlacingMenu();
    }

    // Вызывается при отмене строительства(/установки/посадки)
    private void CancelPlacing()
    {
        if (placablePreview != null)
        {
            placablePreview.Cancel((GridPlace) => OccupyCells(GridPlace));
            placablePreview = null;
        }

        HidePlacingMenu();
    }

    //Вызывается кнопкой "Подтвердить строительство(/установку/посадку)"
    private void PlaceHere()
    {
        PlaceRequest();
    }

    private void PlaceRequest()
    {
        //TODO делаем запрос на сервер, для строительства. Там разруливаем строительство и затраты денег
        if (placablePreview != null)
        {
            placablePreview.IsProcessing = true;
        }

        // Todo вызвать этот метод в ответе с сервера
        PlaceRequestCallback();
    }

    private void PlaceRequestCallback()
    {
        //todo если с сервера прилетел положительный ответ, то делаем это
        bool newPlacable = false;
        if (placablePreview != null) { newPlacable = !placablePreview.IsJustShifting(); }
        
        GroundPlacable placableInstance = InstantiatePlacable();
        if (placableInstance != null)
        {
            if (newPlacable)
            {
                // запрос был с оплатой
                SpendRecources(placableInstance.GetComponent<IShowable>().GetMenuShowItem().Price);
            }
            else
            {
                // запрос без оплаты (просто что поменяли место на гриде, нужно внести в БД)
            }

            HidePlacingMenu();
        }
        else if (placablePreview != null)
        {
            //Иначе вывести сообщение об ошибке (почему не построили)
            placablePreview.IsProcessing = false;
        }
    }

    private GroundPlacable InstantiatePlacable()
    {
        if (placablePreview != null && placablePreview.IsBuildAvailable())
        {
            GroundPlacable placableInstance = placablePreview.InstantiateHere();

            placedThings.Add(placableInstance);
            OccupyCells(placableInstance.GridPlace);

            Destroy(placablePreview.gameObject);

            if (placablePreview != null)
            {
                placablePreview = null;
            }

            return placableInstance;
        }

        return null;
    }

    private List<ControlBtn> GetControllBtns()
    {
        if (controlBtns == null)
        {
            controlBtns = new List<ControlBtn>();

            //todo проверка на тип
            if (placablePreview.PreviewType == PreviewType.BUILDING)
            {
                ControlBtn rotate = new ControlBtn(ControlPanelPart.ROTATE, true, () =>
                {
                    // вращение превьюхи
                    if (placablePreview != null && placablePreview is BuildingPreview)
                    {
                        ((BuildingPreview)placablePreview).Rotate();
                    }
                });

                controlBtns.Add(rotate);
            }

            ControlBtn cancel = new ControlBtn(ControlPanelPart.CANCEL, true, () =>
            {
                // отменить строительство / перемещение
                CancelPlacing();

            });

            ControlBtn accept = new ControlBtn(ControlPanelPart.ACCEPT, true, () =>
            {
                // подтвердить строительство / перемещение
                PlaceHere();
            });

            controlBtns.Add(cancel);
            controlBtns.Add(accept);
        }

        return controlBtns;
    }

    public bool IsResourcesEnough(ProductItemsList products)
    {
        return GetStock().IsResourcesEnough(products);
    }

    public void SpendRecources(ProductItemsList products)
    {
        GetStock().SpendRecources(products);
    }

    public void AddRecources(ProductItemsList products)
    {
        GetStock().AddRecources(products);
    }

    public Camera GetMainCamera()
    {
        return mainCamera;
    }

    private void FreeCells(GridPlace place)
    {
        GetGrid().SetGridPlaceStatus(place, GridPlaceStatus.FREE);
    }

    private void OccupyCells(GridPlace place)
    {
        GetGrid().SetGridPlaceStatus(place, GridPlaceStatus.OCCUPIED);
    }

    private TileMapHolder GetGrid()
    {
        if (grid == null)
        {
            grid = GetComponent<TileMapHolder>();
        }

        return grid;
    }

    private SelectedMenu GetMenu()
    {
        if (menu == null)
        {
            menu = GameObject.FindGameObjectWithTag("SelectedMenu").GetComponent<SelectedMenu>();
        }

        return menu;
    }

    private StockController GetStock()
    {
        if (stock == null)
        {
            stock = StockController.Instance;
        }

        return stock;
    }
}
