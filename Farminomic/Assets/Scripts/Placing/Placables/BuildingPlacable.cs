using System;
using UnityEngine;

public class BuildingPlacable : GroundPlacable
{
    public Sprite Sprite;
    public Sprite SpriteRotated;
    private SpriteRenderer spriteRenderer;

    private bool isRotated;
    private Action<bool> rotatedAdditionAction;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public bool IsRotated()
    {
        return isRotated;
    }

    public void SetRotation(bool isRotated)
    {
        //todo сообщить аниматору о нужной анимации

        this.isRotated = isRotated;
        spriteRenderer.sprite = isRotated ? SpriteRotated : Sprite;

        rotatedAdditionAction?.Invoke(isRotated);
    }

    public virtual void SetOnRotatedAction(Action<bool> action)
    {
        //в случае различий работы здания при разном положении, внести эти различия тут (см грядки)
        rotatedAdditionAction = action;
    }
}
