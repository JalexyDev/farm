using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TimeState))]
public class TimeStateDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        return SpritePropertyDrawer.SpriteSize + 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Duration"));

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var durationRect = new Rect(position.x, position.y, 70, 20);
        var spriteRect = new Rect(position.x + 75, position.y, 70, 20);   

        EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("DurationMinutes"), GUIContent.none);
        EditorGUI.PropertyField(spriteRect, property.FindPropertyRelative("Sprite"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
