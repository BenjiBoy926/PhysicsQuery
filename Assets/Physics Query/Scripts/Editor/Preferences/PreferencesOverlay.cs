using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    [Overlay(typeof(SceneView), Id, "Physics Query Preferences")]
    public class PreferencesOverlay : IMGUIOverlay, ITransientOverlay
    {
        private const string Id = nameof(PreferencesOverlay);
        public bool visible => true;

        public override void OnGUI()
        {
            PreferencesEditor.DrawPropertyField(Preferences.HitSphereRadius);
            PreferencesEditor.DrawPropertyField(Preferences.AlwaysDrawGizmos);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            
            PreferencesEditor.DrawUseDefaultsButton();
            if (GUILayout.Button("Open Preferences"))
            {
                OpenPreferences();
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        private void OpenPreferences()
        {
            SettingsService.OpenUserPreferences(PreferencesProvider.Path);
        }
    }
}