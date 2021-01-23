using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public RectTransform content;

    public GameObject MoveBtn;
    public GameObject RotateBtn;
    public GameObject CancelBtn;
    public GameObject AcceptBtn;
    public GameObject PriceLayout;

    private GameObject priceLayoutInstance;

    public void InitBtns(List<ControlBtn> btns)
    {
        SortControlBtns(ref btns);

        foreach (ControlBtn btn in btns)
        {
            InitControlBtn(btn.Part, btn.Accessible, btn.Action);
        }
    }

    private void InitControlBtn(ControlPanelPart part, bool isAccessible, Action action)
    {
        GameObject partObj = null;

        switch (part)
        {
            case ControlPanelPart.MOVE:
                partObj = Instantiate(MoveBtn);
                break;
            case ControlPanelPart.ROTATE:
                partObj = Instantiate(RotateBtn);
                break;
            case ControlPanelPart.CANCEL:
                partObj = Instantiate(CancelBtn);
                break;
            case ControlPanelPart.ACCEPT:
                partObj = Instantiate(AcceptBtn);
                break;
        }


        if (partObj != null)
        {
            var btn = partObj.GetComponent<Button>();
            if (btn != null)
            {
                //todo вместо этого заменять поведение. Когда наводим показываем подсказку, что можно использовать только при опр условиях.
                btn.interactable = isAccessible;

                btn.onClick.AddListener(() =>
                {
                    action();
                });

                partObj.transform.SetParent(content, false);
            }
        }
    }

    private void SortControlBtns(ref List<ControlBtn> btns)
    {
        btns.Sort((x, y) => x.Part.CompareTo(y.Part));
    }

    public void ShowPrice(int price)
    {
        if (priceLayoutInstance == null)
        {
            priceLayoutInstance = Instantiate(PriceLayout);
            priceLayoutInstance.transform.SetParent(content, false);
        }

        priceLayoutInstance.GetComponentInChildren<Text>().text = price.ToString();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CleanContent();
    }

    private void CleanContent()
    {
        foreach (Transform child in content)
        {
            if (child.gameObject.Equals(priceLayoutInstance))
            {
                priceLayoutInstance = null;
            }

            Destroy(child.gameObject);
        }
    }
}

public enum ControlPanelPart
{
    MOVE, ROTATE, CANCEL, ACCEPT
}
