using System;

public class Function 
{
    public Information Information;
    public Action Action;

    public Function(Information information, Action action)
    {
        this.Information = information;
        this.Action = action;
    }
}
