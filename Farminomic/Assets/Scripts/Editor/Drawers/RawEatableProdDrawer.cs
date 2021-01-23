using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RawEatableProduct))]
public class RawEatableProdDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return SpritePropertyDrawer.SpriteSize + 5;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var nameRect = new Rect(position.x, position.y, 90, 20);
        var priceRect = new Rect(position.x, position.y + 25, 50, 20);
        var countRect = new Rect(position.x + 55, position.y + 25, 30, 20);
        var iconRect = new Rect(position.x + 95, position.y, 150, 20);

        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("prodName"), GUIContent.none);
        EditorGUI.PropertyField(iconRect, property.FindPropertyRelative("icon"), GUIContent.none);
        EditorGUI.PropertyField(priceRect, property.FindPropertyRelative("examplePrice"), GUIContent.none);
        EditorGUI.PropertyField(countRect, property.FindPropertyRelative("currentCount"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
