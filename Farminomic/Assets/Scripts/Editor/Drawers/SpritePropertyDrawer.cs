using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Sprite))]
public class SpritePropertyDrawer : PropertyDrawer
{
    public static float SpriteSize = 60;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent labelN)
    {
        if (property.objectReferenceValue != null)
        {
            return SpriteSize;
        }

        return base.GetPropertyHeight(property, labelN);
    }

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, prop);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        if (prop.objectReferenceValue != null)
        {
            position.width = EditorGUIUtility.labelWidth;

            position.width = SpriteSize;
            position.height = SpriteSize;

            prop.objectReferenceValue =
                EditorGUI.ObjectField(position, prop.objectReferenceValue, typeof(Sprite), false);
        }
        else
        {
            position.width = EditorGUIUtility.labelWidth;
            position.width = SpriteSize;
            position.height = SpriteSize;


            EditorGUI.PropertyField(position, prop, GUIContent.none, false);
        }

        EditorGUI.indentLevel = indent;






























        EditorGUI.EndProperty();
    }
}
