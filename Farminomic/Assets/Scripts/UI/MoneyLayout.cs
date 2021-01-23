using UnityEngine;
using UnityEngine.UI;

public class MoneyLayout : MonoBehaviour
{
    public Text MoneyCount;

    public void ShowMoneyCount(int count)
    {
        MoneyCount.text = count.ToString();
    }
}
