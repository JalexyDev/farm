using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProductController))]
public class ProductControllerInspector : Editor
{
    ProductController productController;

    private void OnEnable()
    {
        productController = (ProductController)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save"))
        {
            EditorMethods.WriteProductNamesEnum("Assets/Scripts/Enums/", "ProductNames", productController.Products);
        }
    }
}
