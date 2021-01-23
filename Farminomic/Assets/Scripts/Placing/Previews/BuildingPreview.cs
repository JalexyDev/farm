
using UnityEngine;

public class BuildingPreview : GroundPlacablePreview
{
    public Vector2Int SizeRotated;
    public Sprite Sprite;
    public Sprite SpriteRotated;

    private bool isRotated;

    public bool IsRotated { get => isRotated; 
        set
        {
            isRotated = value;
            MainRenderer.sprite = IsRotated ? SpriteRotated : Sprite;
        } 
    }

    public override Vector2Int GetSize()
    {
        return IsRotated ? SizeRotated : Size;
    }

    public void Rotate()
    {
        IsRotated = !IsRotated;
        MainRenderer.sprite = IsRotated ? SpriteRotated : Sprite;
    }
}
