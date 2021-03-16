using System;
using UnityEngine;

public class BuildingPlacable : GroundPlacable
{
    public Sprite Sprite;
    public Sprite SpriteRotated;
    public float ColliderOffsetX;
    public float ColliderOffsetXRotated;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    private bool isRotated;
    private Action<bool> rotatedAdditionAction;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
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
        float offsetX = isRotated ? ColliderOffsetXRotated : ColliderOffsetX;
        collider.offset = new Vector2(offsetX, collider.offset.y);

        rotatedAdditionAction?.Invoke(isRotated);
    }

    public virtual void SetOnRotatedAction(Action<bool> action)
    {
        //в случае различий работы здания при разном положении, внести эти различия тут (см грядки)
        rotatedAdditionAction = action;
    }
}
