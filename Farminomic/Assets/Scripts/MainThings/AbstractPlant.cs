using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractPlant : MonoBehaviour, IShowable
{
    [Header("Информации о растении")]
    public MenuShowItem menuShowItem;

    [Header("Получаемые продукты")]
    public ProductItemsList InterruptProducts;
    public ProductItemsList FinishProducts;

    [Header("Множители получаемых продуктов")]
    public float MinFactor = 1;
    public float MaxFactor = 1.1f;
    public float StepForFactor;

    protected StateSwitcher stateSwitcher;
    protected PlantsController controller;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        InitFunctions();
        InitControlBtns();
        InitPlantDetails();
    }

    private void Start()
    {
        stateSwitcher = GetStateSwitcher();
        if (stateSwitcher != null)
        {
            stateSwitcher.SetBetweenStatesAction(GetBetweenStateAction());
            stateSwitcher.SetLastStateAction(GetLastStateAction());
            stateSwitcher.SetFinishAction(GetFinishStateAction());

            stateSwitcher.StartSwitching();
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
           
        OnClick();
    }

    public MenuShowItem GetMenuShowItem()
    {
        return menuShowItem;
    }

    protected abstract void InitPlantDetails();

    protected abstract Action<TimeState> GetBetweenStateAction();

    protected abstract Action<TimeState> GetLastStateAction();

    protected abstract Action<TimeState> GetFinishStateAction();


    protected virtual void OnClick()
    {
        if (Controls.OpenMenuDisabled)
        {
            return;
        }

        GetPlantsController().CloseMenu();
        GetPlantsController().ShowMenu(menuShowItem);
    }

    protected virtual void InitFunctions()
    {
        // Todo функции: удобрение, полив, собрать и т.п.
        //menuShowItem.AddFunctionList(fun1, fun2, ...);

    }

    protected virtual void InitControlBtns()
    {
        ControlBtn delete = new ControlBtn(ControlPanelPart.CANCEL, true, () =>
        {
            //todo удалить растение без компенсации

        });

        menuShowItem.AddControlBtn(delete);
    }

    protected void SetSprite(Sprite sprite)
    {
        GetSpriteRenderer().sprite = sprite;
    }

    protected void SelectState(int stateNumber)
    {
        if (GetStateSwitcher() != null)
        {
            GetStateSwitcher().SelectState(stateNumber);
        }
    }

    protected PlantsController GetPlantsController()
    {
        if (controller == null)
        {
            controller = PlantsController.Instance;
        }

        return controller;
    }

    private SpriteRenderer GetSpriteRenderer()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        return spriteRenderer;
    }

    private StateSwitcher GetStateSwitcher()
    {
        if (stateSwitcher == null)
        {
            stateSwitcher = GetComponent<StateSwitcher>();
        }

        return stateSwitcher;
    }
}
