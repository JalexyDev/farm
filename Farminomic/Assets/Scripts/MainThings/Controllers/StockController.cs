using UnityEngine;

public class StockController : MonoBehaviour
{
    public static StockController Instance;
    
    public Money Money;
    private MoneyLayout moneyLayout;

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

        DontDestroyOnLoad(gameObject);

        InitStock();
    }

    private void Start()
    {
        GetMoneyLayout().ShowMoneyCount(Money.Count);
    }

    //todo считываем данные из БД
    private void InitStock()
    {
        
    }

    public bool SpendMoney(int count)
    {
        if (IsMoneyEnough(count))
        {
            Money.Spend(count);
            GetMoneyLayout().ShowMoneyCount(Money.Count);

            return true;
        }

        return false;
    }

    public bool IsMoneyEnough(int count)
    {
        return Money.IsEnoughForExchange(count);
    }

    private MoneyLayout GetMoneyLayout()
    {
        if (moneyLayout == null)
        {
            moneyLayout = GameObject.FindGameObjectWithTag("MainInterface").GetComponentInChildren<MoneyLayout>();
        }

        return moneyLayout;
    }
}
