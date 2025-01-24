using System;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PreferencesEditor
    {
        public static void DrawFullInspectorGUI()
        {
            Preferences.Properties.ForEach(DrawPropertyField);
            DrawUseDefaultsButton();
        }
        public static void DrawUseDefaultsButton()
        {
            if (GUILayout.Button("Use Defaults"))
            {
                Preferences.Clear();
            }
        }
        public static void DrawPropertyField(PreferenceProperty property)
        {
            if (property.PropertyType == typeof(bool))
            {
                DrawFieldForProperty<bool>(property, BoolField);
            }
            else if (property.PropertyType == typeof(Color))
            {
                DrawFieldForProperty<Color>(property, ColorField);
            }
            else if (property.PropertyType == typeof(float))
            {
                DrawFieldForProperty<float>(property, FloatField);
            }
            else
            {
                EditorGUILayout.HelpBox($"Unable to edit property '{property.Name}' " +
                    $"because the control for type '{property.PropertyType.Name}' has not been implemented",
                    MessageType.Error);
            }
        }
        private static void DrawFieldForProperty<TValue>(PreferenceProperty property, Func<string, TValue, TValue> drawField)
        {
            string label = ObjectNames.NicifyVariableName(property.Name);
            property.ObjectValue = drawField(label, (TValue)property.ObjectValue);
        }
        private static bool BoolField(string label, bool current)
        {
            return EditorGUILayout.Toggle(label, current);
        }
        private static Color ColorField(string label, Color current)
        {
            return EditorGUILayout.ColorField(label, current);
        }
        private static float FloatField(string label, float current)
        {
            return EditorGUILayout.FloatField(label, current);
        }
    }
}