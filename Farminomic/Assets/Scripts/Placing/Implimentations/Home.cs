
using UnityEngine;

public class Home : Building
{
    private void Start()
    {
        foreach (Building building in GetBuildingController().Buildings)
        {
            Function function = new Function(building.menuShowItem.Information, () =>
            {
                if (GetBuildingController().IsResourcesEnough(building.menuShowItem.Price))
                {
                    Camera camera = GetBuildingController().GetMainCamera();

                    //todo выбрать оптимальную позицию для респа превью строящихся зданий
                    GetBuildingController().StartPlacing(building.InstPreviewHere(new Vector3(camera.transform.position.x, camera.transform.position.y, 0)));
                }
            }
                );

            menuShowItem.AddFunction(function);
        }
    }
}
