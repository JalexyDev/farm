using System;
using System.Diagnostics;

public class BedsPlant : AbstractPlant
{
    // Отдельный префаб, задается в инспекторе
    public BedsPlantPreview Preview;

    // Компонент этого объекта. Отвечает за хранение позиции и перемещение
    private BedsPlantPlacable placable;

    public BedsPlantPlacable Placable { get => GetPlacable(); }

    public BedsPlantPlacable GetPlacable()
    {
        if (placable == null)
        {
            placable = GetComponent<BedsPlantPlacable>();
        }

        return placable;
    }

    protected override void InitPlantDetails()
    {
        //todo если что-то нужно сделать в Awake делаем тут
    }

    protected override void InitFunctions()
    {
        //todo сделать menuShowItem-ы для разных функций.

        Function function = new Function(menuShowItem.Information, () =>
        {
            print("Func");
        } );

        menuShowItem.AddFunctionList(function);
    }

    protected override Action<TimeState> GetBetweenStateAction()
    {
        return (TimeState) =>
        {
            SetSprite(TimeState.Sprite);
            canHarvest = false;
            currentAdditionFactor += StepForFactor;
        };
    }

    protected override Action<TimeState> GetLastStateAction()
    {
        return (TimeState) =>
        {
            SetSprite(TimeState.Sprite);
            canHarvest = true;
            //todo установить награду за сбор, заменить кнопку "Прервать рост" на "Собрать урожай"
        };
    }

    protected override Action<TimeState> GetFinishStateAction()
    {
        return (TimeState) =>
        {
            GetPlantsController().AddRecources(InterruptProducts * (MaxFactor + currentAdditionFactor));
            currentAdditionFactor = 0;
            canHarvest = false;
            Destroy(gameObject);
        };
    }
}
