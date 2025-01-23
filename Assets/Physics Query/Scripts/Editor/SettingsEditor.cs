using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(Settings))]
    public class SettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawGUI(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }
        public static void DrawGUI(SerializedObject serializedSettings)
        {
            serializedSettings.Update();
            SerializedProperty iterator = serializedSettings.GetIterator();
            iterator.NextVisible(true);
            iterator.NextVisible(false);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUIUtility.labelWidth = 200;
            do
            {
                EditorGUILayout.PropertyField(iterator, true);
            }
            while (iterator.NextVisible(false));

            if (GUILayout.Button("Restore Defaults"))
            {
                Settings settings = serializedSettings.targetObject as Settings;
                settings.Reset();
                serializedSettings.Update();
            }

            EditorGUILayout.EndVertical();
        }
    }
}