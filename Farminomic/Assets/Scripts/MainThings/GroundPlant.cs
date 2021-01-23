using System;
using UnityEngine;

public class GroundPlant : AbstractPlant
{
    // номер стейта "Взрослое без плодов"
    public int StateIndexWithoutProduct;

    // Отдельный префаб, задается в инспекторе
    public GroundPlantPreview Preview;

    // Компонент этого объекта. Отвечает за хранение позиции и перемещение
    private GroundPlacable Placable;

    protected override void InitPlantDetails()
    {
        Placable = GetComponent<GroundPlacable>();
    }

    protected override Action<TimeState> GetBetweenStateAction()
    {
        return (TimeState) =>
        {
            SetSprite(TimeState.Sprite);
            //todo установить какой мусор будет получен при прерывании роста.
        };
    }

    protected override Action<TimeState> GetLastStateAction()
    {
        return (TimeState) =>
        {
            SetSprite(TimeState.Sprite);
            //todo установить награду за сбор, заменить кнопку "Прервать рост" на "Собрать урожай"
        };
    }

    protected override Action<TimeState> GetFinishStateAction()
    {
        return (TimeState) =>
        {
            //todo сменить стейт на взрослое растение без плодов
            SelectState(StateIndexWithoutProduct);
        };
    }

    protected override void InitControlBtns()
    {
        base.InitControlBtns();

        //todo определять accessibility по статусу (типа если нет подписки, перемещать нельзя но видно заблоченую кнопку)
        ControlBtn move = new ControlBtn(ControlPanelPart.MOVE, true, () =>
        {
            Placable.StartShifting(InstPreviewHere(transform.position), GetPlantsController());
        });

        menuShowItem.AddControlBtn(move);
    }

    public GroundPlantPreview InstPreviewHere(Vector3 pose)
    {
        GroundPlantPreview prevInstance = Instantiate(Preview, pose, Quaternion.identity);

        prevInstance.PlacablePrice = menuShowItem.Price;
        prevInstance.LastPlacablePlace = Placable.GridPlace;

        prevInstance.PlacableFunc = (isRotated, gridPose) =>
        {
            GroundPlacable placable;
            if (prevInstance.IsJustShifting())
            {
                placable = GetComponent<GroundPlacable>();
                placable.transform.position = prevInstance.transform.position;
            }
            else
            {
                placable = Instantiate(Placable, prevInstance.transform.position, Quaternion.identity);
            }

            placable.GridPlace = gridPose;
            return placable;
        };

        return prevInstance;
    }
}
