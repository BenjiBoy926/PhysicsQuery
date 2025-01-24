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
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            PreferencesEditor.DrawInspectorGUI();
            EditorGUILayout.EndVertical();
        }
    }
}