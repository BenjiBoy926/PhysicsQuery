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
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUIUtility.labelWidth = 200;
            for (int i = 0; i < Preferences.Properties.Length; i++)
            {
                DrawFieldForProperty(Preferences.Properties[i]);
            }
            if (GUILayout.Button("Restore Defaults"))
            {
                Preferences.Clear();
            }
            EditorGUILayout.EndVertical();
        }
        private void DrawFieldForProperty(PreferenceProperty property)
        {
            if (property.Name == nameof(Preferences.HitSphereRadiusProportion))
            {
                DrawFieldForProperty<float>(property, FloatSlider01);
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
        private void DrawFieldForProperty<TValue>(PreferenceProperty property, Func<string, TValue, TValue> drawField)
        {
            string label = ObjectNames.NicifyVariableName(property.Name);
            property.ObjectValue = drawField(label, (TValue)property.ObjectValue);
        }
        private Color ColorField(string label, Color current)
        {
            return EditorGUILayout.ColorField(label, current);
        }
        private float FloatField(string label, float current)
        {
            return EditorGUILayout.FloatField(label, current);
        }
        private float FloatSlider01(string label, float current)
        {
            return EditorGUILayout.Slider(label, current, 0, 1);
        }
    }
}