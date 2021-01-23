using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

// это область канваса. Она хранит в себе ссылки на Info и Fuction panels и передает им запросы
// от выбранного объекта (вывести инфу, отобразить функциональные кнопки и т.п.)
public class SelectedMenu : MonoBehaviour
{
    public InfoPanel InfoPanel;
    public FunctionPanel FunctionPanel;
    public ControlPanel ControlPanel;

    private MenuShowItem lastMenuShowItem;

    public void ShowRootMenu(MenuShowItem item)
    {
        lastMenuShowItem = item;
        lastMenuShowItem.Price = 0;
        ShowMenu(item);
    }

    public void ShowMenu(MenuShowItem item)
    {
        if (item.Information != null)
        {
            ShowInfo(item.Information);
        }

        if (item.Functions != null && item.Functions.Count != 0)
        {
            ShowFunctions(item.Functions);
        }

        if (item.ControlBtns != null && item.ControlBtns.Count != 0)
        {
            ShowControls(item.ControlBtns);
        }

        if (item.Price != 0)
        {
            ShowPrice(item.Price);
        }
    }

    public void CloseRootMenu()
    {
        CloseMenu();
        lastMenuShowItem = null;
    }

    public void CloseMenu()
    {
        InfoPanel.Close();
        FunctionPanel.Close();
        ControlPanel.Close();

        if (lastMenuShowItem != null)
        {
            ShowRootMenu(lastMenuShowItem);
        }
    }

    private void ShowInfo(Information information)
    {
        InfoPanel.gameObject.SetActive(true);
        InfoPanel.SetInformation(information);
    }

    private void ShowFunctions(List<Function> functions)
    {
        FunctionPanel.gameObject.SetActive(true);
        FunctionPanel.ShowFunctions(functions);
    }

    public void ShowPrice(int price)
    {
        ControlPanel.gameObject.SetActive(true);
        ControlPanel.ShowPrice(price);
    }

    private void ShowControls(List<ControlBtn> controlBtns)
    {
        ControlPanel.Close();

        ControlPanel.gameObject.SetActive(true);
        ControlPanel.InitBtns(controlBtns);
    }
}
