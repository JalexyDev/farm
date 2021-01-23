using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundPlacablePreview : Preview
{
    public PreviewType PreviewType;
    public Func<bool, GridPlace, GroundPlacable> PlacableFunc;

    public Vector2Int Size;
    
    private GridPlace placablePlace;

    private Vector2Int currentGridPose;

    private bool isJustShifting = false;
    private bool isBuildAvailable;
    private bool isMoving;

    protected SpriteRenderer MainRenderer;
    private Color green;
    private Color red;

    public GridPlace LastPlacablePlace { get => placablePlace; set => placablePlace = value; }


    private void Awake()
    {
        MainRenderer = GetComponentInChildren<SpriteRenderer>();
        green = new Color(0, 1f, .3f, .8f);
        red = new Color(1, .2f, .2f, .8f);

        isJustShifting = false;
    }

    private void Start()
    {
        Controls.OpenMenuDisabled = true;
    }

    private void OnDestroy()
    {
        Controls.OpenMenuDisabled = false;
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        isMoving = true;
    }

    private void OnMouseUp()
    {
        isMoving = false;
    }

    public void SetCurrentMousePosition(Vector2 position, Vector2Int GridPose, Func<Boolean> isBuildAvailable)
    {
        if (isMoving && !IsProcessing)
        {
            transform.position = position;
            currentGridPose = GridPose;
            SetBuildAvailable(isBuildAvailable());
        }
    }

    public void SetSpawnPosition(Vector2Int GridPose)
    {
        currentGridPose = GridPose;
    }

    public GroundPlacable InstantiateHere()
    {
        if (isBuildAvailable)
        {
            Vector2Int size = GetSize();

            Cell[] placeInGrid = new Cell[size.x * size.y];
            int index = 0;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    placeInGrid[index++] = new Cell(currentGridPose.x + x, currentGridPose.y + y);
                }
            }

            bool isRotated = false;
            if (PreviewType == PreviewType.BUILDING)
            {
                isRotated = ((BuildingPreview)this).IsRotated;
            }

            GroundPlacable placable = PlacableFunc(isRotated, new GridPlace(placeInGrid));

            Destroy(gameObject);

            return placable;
        }

        return null;
    }

    public void Cancel(Action<GridPlace> action)
    {
        if (IsJustShifting())
        {
            // todo сказать Placable чтобы вернуло свое старое место на гриде
            action(LastPlacablePlace);

        }

        Destroy(gameObject);
    }

    public void SetBuildAvailable(bool available)
    {
        isBuildAvailable = available;
        MainRenderer.material.color = available ? green : red;
    }

    public bool IsBuildAvailable()
    {
        return isBuildAvailable;
    }

    public void SetIsJustShifting(bool isShifting)
    {
        this.isJustShifting = isShifting;
    }

    public bool IsJustShifting()
    {
        return isJustShifting;
    }

    public virtual Vector2Int GetSize()
    {
        return Size;
    }
}

public enum PreviewType
{
    BUILDING, GROUND_PLANT
}
