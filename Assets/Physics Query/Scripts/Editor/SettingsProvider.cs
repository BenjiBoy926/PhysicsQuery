using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhysicsQuery.Editor
{
    public class SettingsProvider : UnityEditor.SettingsProvider
    {
        private const string Path = "Project/" + Settings.PackageQualifiedName;

        private Settings _settings;
        private SerializedObject _serializedSettings;

        public SettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {

        }
        [SettingsProvider]
        public static UnityEditor.SettingsProvider CreateSettingsProvider()
        {
            SerializedObject serializedSettings = new SerializedObject(Settings.GetInstance());
            IEnumerable<string> keywords = GetSearchKeywordsFromSerializedObject(serializedSettings);
            return new SettingsProvider(Path, SettingsScope.Project, keywords);
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);
            _settings = Settings.GetInstance();
            _serializedSettings = new SerializedObject(_settings);
        }
        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);

            SerializedProperty iterator = _serializedSettings.GetIterator();
            iterator.NextVisible(true);
            iterator.NextVisible(false);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUIUtility.labelWidth = 200;
            do
            {
                GUI.enabled = iterator.name != "m_Script";
                EditorGUILayout.PropertyField(iterator, true);
            } 
            while (iterator.NextVisible(false));
            GUI.enabled = true;

            if (GUILayout.Button("Restore Defaults"))
            {
                _settings.Reset();
                _serializedSettings.Update();
            }

            EditorGUILayout.EndVertical();

            _serializedSettings.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}