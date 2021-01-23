using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ProductParams))]
public class ProductParamsDrawer : PropertyDrawer
{
    private SerializedProperty name, icon, price;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return SpritePropertyDrawer.SpriteSize + 5;
    }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        name = property.FindPropertyRelative("Name");
        icon = property.FindPropertyRelative("Icon");
        price = property.FindPropertyRelative("ExamplePrice");

        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var nameRect = new Rect(position.x, position.y, 90, 20);
        var priceRect = new Rect(position.x, position.y + 25, 50, 20);
        var iconRect = new Rect(position.x + 95, position.y, 60, 60);

        EditorGUI.PropertyField(nameRect, name, GUIContent.none);
        EditorGUI.PropertyField(iconRect, icon, GUIContent.none);
        EditorGUI.PropertyField(priceRect, price, GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
