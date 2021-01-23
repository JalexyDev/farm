using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Information)), CanEditMultipleObjects]
public class InformationInspector : Editor
{
    SerializedProperty description;

    void OnEnable()
    {
        description = serializedObject.FindProperty("Description");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        description.stringValue = EditorGUILayout.TextArea(description.stringValue, GUILayout.MaxHeight(100));
        serializedObject.ApplyModifiedProperties();
    }
}
