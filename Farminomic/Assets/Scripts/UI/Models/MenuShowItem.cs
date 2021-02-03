using System;
using System.Collections.Generic;

[Serializable]
public class MenuShowItem : IValuable
{
    public ProductItemsList ShowingPrice;
    public Information Information;
    private List<Function> functions;
    private List<ControlBtn> controlBtns;

    public MenuShowItem(Information information, List<Function> functions, List<ControlBtn> controlBtns, ProductItemsList showingPrice = null)
    {
        Information = information;
        this.functions = functions;
        this.controlBtns = controlBtns;
        ShowingPrice = showingPrice;
    }

    public MenuShowItem() { }

    public List<Function> Functions { get => functions; set => functions = value; }
    public List<ControlBtn> ControlBtns { get => controlBtns; set => controlBtns = value; }

    public ProductItemsList Price { get => ShowingPrice; set => ShowingPrice = value; }

    public void AddFunction(Function function)
    {
        if (functions == null)
        {
            functions = new List<Function>();
        }

        functions.Add(function);
    }

    public void AddFunctionList(params Function[] functions)
    {
        foreach (Function fun in functions)
        {
            AddFunction(fun);
        }
    }

    public void AddControlBtn(ControlBtn btn)
    {
        if (controlBtns == null)
        {
            controlBtns = new List<ControlBtn>();
        }

        if (ContainsControlBtn(btn))
        {
            int index = controlBtns.IndexOf(btn);
            controlBtns.RemoveAt(index);
            controlBtns.Insert(index, btn);
        }
        else
        {
            controlBtns.Add(btn);
        }
    }

    public void AddControlBtnList(params ControlBtn[] btns)
    {
        foreach (ControlBtn btn in btns)
        {
            AddControlBtn(btn);
        }
    }

    public bool ContainsControlBtn(ControlBtn btn)
    {
        return controlBtns != null && controlBtns.Contains(btn);
    }
}
