using System.Collections.Generic;

public class BuildingController : BaseController
{
    public static BuildingController Instance;
    public List<Building> Buildings;

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

    //todo учитывать построенные здания и реализовать ограничение по количеству построект определенных зданий

}
