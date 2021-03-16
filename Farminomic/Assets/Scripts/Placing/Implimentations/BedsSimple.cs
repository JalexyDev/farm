using System.Collections.Generic;
using UnityEngine;

public class BedsSimple : Building
{
    public Transform[] placesForPlant;
    public Transform[] placesForPlantRotated;
    private BedsPlantPlacable[] planted;
    private BedsPlant[] potentiallyPlant;
    private BedsPlantPreview[] previews;
    private ProductItemsList AcceptPrice;

    private PlantsController plantsController;

    private void Start()
    {
        planted = new BedsPlantPlacable[placesForPlant.Length];
        potentiallyPlant = new BedsPlant[placesForPlant.Length];
        previews = new BedsPlantPreview[placesForPlant.Length];

        GetPlacable().SetOnRotatedAction((setRotatedPos) =>
        {
            Transform[] rotatedPlaces = setRotatedPos ? placesForPlantRotated : placesForPlant;

            for (int i = 0; i < planted.Length; i++)
            {
                if (planted[i] != null)
                {
                    planted[i].transform.position = rotatedPlaces[i].transform.position;
                }
            }
        });
    }

    protected override void InitFunctions()
    {
        menuShowItem.Functions = new List<Function>();

        foreach (BedsPlant plant in GetPlantsController().GetAvailableBedsPlantPreviews(this))
        {
            Function function = new Function(plant.menuShowItem.Information, () =>
            {
                if (GetPlantsController().IsResourcesEnough(plant.menuShowItem.Price))
                {
                    PlacePlantPreview(plant);
                }

                //todo выделить цену красным или т.п.
                ShowPlacingMenu(plant);
            });

            menuShowItem.AddFunction(function);
        }
    }

    private void ShowPlacingMenu(BedsPlant plant)
    {
        MenuShowItem placingMenuItem = new MenuShowItem(plant.menuShowItem.Information, null, null, AcceptPrice);

        ControlBtn accept = new ControlBtn(ControlPanelPart.ACCEPT, AcceptPrice != null && GetPlantsController().IsResourcesEnough(AcceptPrice), () =>
        {
            AcceptPlacing();
        });

        ControlBtn decline = new ControlBtn(ControlPanelPart.CANCEL, true, () =>
        {
            DeclinePlacing();
        });

        placingMenuItem.AddControlBtnList(accept, decline);

        GetBuildingController().ShowMenu(placingMenuItem);
    }

    public void PlacePlantPreview(BedsPlant plant)
    {
        //todo при размещении учитывать стоимость. И если стоимость выше баланса, как то на это намекнуть.

        Transform[] places = GetPlacable().IsRotated() ? placesForPlantRotated : placesForPlant;

        for (int i = 0; i < potentiallyPlant.Length; i++)
        {
            if (potentiallyPlant[i] != null || planted[i] != null) { continue; }

            potentiallyPlant[i] = plant;

            Vector3 position = places[i].position;
            previews[i] = Instantiate(plant.Preview, position, Quaternion.identity);

            if (AcceptPrice == null)
            {
                AcceptPrice = new ProductItemsList();
            }

            AcceptPrice += plant.menuShowItem.Price;

            break;
        }
    }

    //todo вызывать при удалении растения с грядки (когда выросло или когда захотелось)
    public void RemovePlant(BedsPlant plant)
    {
        //todo проверить стадию роста. Если растение не выросло, значит это оборт
        //если оборт - просто удаляем растение с грядки и из массива
        //иначе в Сток помещаем вросшую продукцию и потом удаляем
    }

    // Вызывается на кнопку "Подтвердить посадку"
    public void AcceptPlacing()
    {
        //todo выполнять если стоимость <= баланса. Иначе намекнуть что нельзя.
        if (GetPlantsController().IsResourcesEnough(AcceptPrice))
        {
            BedsPlantPreview preview;

            for (int i = 0; i < potentiallyPlant.Length; i++)
            {
                preview = previews[i];

                if (preview != null)
                {
                    BedsPlantPlacable plantInstance = Instantiate(potentiallyPlant[i].Placable, preview.transform.position, Quaternion.identity);
                    plantInstance.transform.SetParent(this.transform);
                    plantInstance.RespPose = placesForPlant[i];

                    //todo придумать как лучше потратить деньги. Сделать статический Stock и чтобы он сам отправлял на сервер запросы
                    //todo сообщить серверу о посадке (чтобы добавить в БД)

                    GetPlantsController().SpendRecources(AcceptPrice);

                    AcceptPrice = null;

                    planted[i] = plantInstance;
                    potentiallyPlant[i] = null;
                    previews[i] = null;

                    Destroy(preview.gameObject);
                }
            }

            HidePlacingMenu();
        }
        else
        {
            //todo 
        }
    }

    private void DeclinePlacing()
    {
        BedsPlantPreview preview;
        for (int i = 0; i < potentiallyPlant.Length; i++)
        {
            preview = previews[i];

            if (preview != null)
            {
                AcceptPrice = null;

                potentiallyPlant[i] = null;
                previews[i] = null;

                Destroy(preview.gameObject);
            }
        }

        HidePlacingMenu();
    }

    private void HidePlacingMenu()
    {
        GetPlantsController().CloseMenu();
    }

    private PlantsController GetPlantsController()
    {
        if (plantsController == null)
        {
            plantsController = PlantsController.Instance;
        }

        return plantsController;
    }
}
