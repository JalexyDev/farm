using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProductNames))]
public class ProductNamesWriter : Editor
{
    ProductNames productNames;

    private void OnEnable()
    {
        productNames = (ProductNames)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save"))
        {
            EditorMethods.WriteToEnum("Assets/Scripts/Enums/", "RawEatableProdName", productNames.rawEatableProducts);
            EditorMethods.WriteToEnum("Assets/Scripts/Enums/", "RawMaterialProdName", productNames.rawMaterialProducts);
            EditorMethods.WriteToEnum("Assets/Scripts/Enums/", "EatableProdName", productNames.eatableProducts);
            EditorMethods.WriteToEnum("Assets/Scripts/Enums/", "LiquidEatableProdName", productNames.liquidEtableProducts);
            EditorMethods.WriteToEnum("Assets/Scripts/Enums/", "MaterialProdName", productNames.materialProducts);

            productNames.SaveAllProducts();
        }
    }
}
