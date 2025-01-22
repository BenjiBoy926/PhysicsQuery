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
            EditorGUI.indentLevel++;
            for (int i = 0; i < properties.Length; i++)
            {
                DrawColorFieldForProperty(properties[i]);
            }
            EditorGUI.indentLevel--;
        }
        private void DrawColorFieldForProperty(PropertyInfo property)
        {
            string label = ObjectNames.NicifyVariableName(property.Name);
            Color current = (Color)property.GetValue(null);
            current = EditorGUILayout.ColorField(label, current);
            property.SetValue(null, current);
        }
    }
}