using System;

public class ControlBtn
{
    public ControlPanelPart Part;
    public bool Accessible;
    public Action Action;

    public ControlBtn (ControlPanelPart part, bool accessible, Action action)
    {
        Part = part;
        Accessible = accessible;
        Action = action;
    }

    public override bool Equals(object obj)
    {
        return obj is ControlBtn btn &&
               Part == btn.Part;
    }

    public override int GetHashCode()
    {
        return -1954173868 + Part.GetHashCode();
    }
}
