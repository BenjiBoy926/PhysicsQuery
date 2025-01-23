using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PreferencesProvider : UnityEditor.SettingsProvider
    {
        private const string Path = "Preferences/Physics Query Preferences";

        public PreferencesProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
        }
        [SettingsProvider]
        public static UnityEditor.SettingsProvider CreateSettingsProvider()
        {
            return new PreferencesProvider(Path, SettingsScope.User, Keywords());
        }
        private static IEnumerable<string> Keywords()
        {
            PropertyInfo[] properties = typeof(Preferences).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                yield return ObjectNames.NicifyVariableName(properties[i].Name);
            }
        }

        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);
            DrawColorFieldForEachProperty();
        }
        private void DrawColorFieldForEachProperty()
        {
            PropertyInfo[] properties = typeof(Preferences).GetProperties();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUIUtility.labelWidth = 200;
            for (int i = 0; i < properties.Length; i++)
            {
                DrawFieldForProperty(properties[i]);
            }
            EditorGUILayout.EndVertical();
        }
        private void DrawFieldForProperty(PropertyInfo property)
        {
            if (property.PropertyType == typeof(Color))
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
        private void DrawFieldForProperty<TValue>(PropertyInfo property, Func<string, TValue, TValue> drawField)
        {
            string label = ObjectNames.NicifyVariableName(property.Name);
            TValue current = (TValue)property.GetValue(null);
            current = drawField(label, current);
            property.SetValue(null, current);
        }
        private Color ColorField(string label, Color current)
        {
            return EditorGUILayout.ColorField(label, current);
        }
        private float FloatField(string label, float current)
        {
            return EditorGUILayout.FloatField(label, current);
        }
    }
}