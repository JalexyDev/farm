using System.Collections.Generic;

public class PlantsController : BaseController
{
    public static PlantsController Instance;
    public List<BedsPlant> BedsPlants;
    public List<GroundPlant> GroundPlants;

    //todo разобраться с посадкой наземных растений.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    public List<BedsPlant> GetAvailableBedsPlantPreviews(BedsSimple beds)
    {
        List<BedsPlant> availablePreviews = new List<BedsPlant>();

        foreach (BedsPlant plant in BedsPlants)
        {
            //todo if (это растение уже доступно и его можно садить на beds (определить улучшеная или нет))
            availablePreviews.Add(plant);
        }

        return availablePreviews;
    }

    public List<GroundPlant> GetAvailableGroundPlantPreviews()
    {
        List<GroundPlant> availablePreviews = new List<GroundPlant>();

        foreach (GroundPlant preview in GroundPlants)
        {
            //todo if (это растение уже доступно)
            availablePreviews.Add(preview);
        }

        return availablePreviews;
    }
}
