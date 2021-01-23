using System;
using UnityEngine;

[Serializable]
public class Information
{
    public string Name;
    [TextArea(7, 10)]
    public string Description;
    public Sprite Icon;
}
