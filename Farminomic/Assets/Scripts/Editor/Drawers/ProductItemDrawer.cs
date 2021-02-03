using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ProductItem))]
public class ProductItemDrawer : PropertyDrawer
{
    private SerializedProperty prodName;
    private SerializedProperty count;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        prodName = property.FindPropertyRelative("Name");
        count = property.FindPropertyRelative("Count");

        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
       
        position = EditorGUI.PrefixLabel(position,
            GUIUtility.GetControlID(FocusType.Passive),
            label);
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect prodNameRect = new Rect(
            position.x ,
            position.y,
            position.width * 0.8f - 5,
            position.height);

        Rect countRect = new Rect(
            position.x + position.width * 0.8f,
            position.y,
            position.width * 0.2f,
            position.height);

        EditorGUI.PropertyField(prodNameRect, prodName, GUIContent.none);
        EditorGUI.PropertyField(countRect, count, GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
