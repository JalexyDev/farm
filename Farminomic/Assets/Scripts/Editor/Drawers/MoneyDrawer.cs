using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Money))]
public class MoneyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return SpritePropertyDrawer.SpriteSize + 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var labelRect = new Rect(position.x, position.y, 70, 20);
        
        var priceLabelRect = new Rect(position.x, position.y + 25, 80, 20);
        var priceRect = new Rect(position.x + 90, position.y + 25, 70, 20);
        
        var countLabelRect = new Rect(position.x, position.y + 50, 80, 20);
        var countRect = new Rect(position.x + 90, position.y + 50, 70, 20);
        
        var iconRect = new Rect(position.x + 175, position.y + 20, 150, 20);

        EditorGUI.LabelField(labelRect, new GUIContent("Денюшки"));
        EditorGUI.LabelField(priceLabelRect, new GUIContent("Цена"));
        EditorGUI.LabelField(countLabelRect, new GUIContent("Количество"));
        EditorGUI.PropertyField(iconRect, property.FindPropertyRelative("icon"), GUIContent.none);
        EditorGUI.PropertyField(priceRect, property.FindPropertyRelative("examplePrice"), GUIContent.none);
        EditorGUI.PropertyField(countRect, property.FindPropertyRelative("currentCount"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
