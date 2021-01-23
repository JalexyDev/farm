using System;

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
            //todo удалить с грядки мб перенести "Мусор" игроку
            Destroy(gameObject);
        };
    }
}
