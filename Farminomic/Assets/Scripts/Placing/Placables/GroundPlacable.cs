

public class GroundPlacable : Placable
{   
    private GridPlace place;
    public GridPlace GridPlace { get => place; set => place = value; }

    public void StartShifting(GroundPlacablePreview preview, BaseController controller)
    {
        preview.SetIsJustShifting(true);
        controller.StartPlacing(preview);
    }
}
