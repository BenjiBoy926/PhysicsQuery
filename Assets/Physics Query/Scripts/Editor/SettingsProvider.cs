using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PQuery.Editor
{
    public class SettingsProvider : UnityEditor.SettingsProvider
    {
        private const string Path = "Project/" + Settings.PackageQualifiedName;

        private SerializedObject _serializedSettings;

        public SettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {

        }
        [SettingsProvider]
        public static UnityEditor.SettingsProvider CreateSettingsProvider()
        {
            SerializedObject serializedSettings = GetSerializedSettings();
            IEnumerable<string> keywords = GetSearchKeywordsFromSerializedObject(serializedSettings);
            return new SettingsProvider(Path, SettingsScope.Project, keywords);
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);
            _serializedSettings = GetSerializedSettings();
        }
        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);
            EditorGUIUtility.labelWidth = 200;
            SettingsEditor.DrawGUI(_serializedSettings);
            _serializedSettings.ApplyModifiedPropertiesWithoutUndo();
        }

        private static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(Settings.GetInstance());
        }
    }
}