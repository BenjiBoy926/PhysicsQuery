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

            EditorGUILayout.BeginHorizontal();
            PreferencesEditor.DrawUseDefaultsButton();
            if (GUILayout.Button("Open Preferences"))
            {
                OpenPreferences();
            }
            EditorGUILayout.EndHorizontal();
        }
        private void OpenPreferences()
        {
            SettingsService.OpenUserPreferences(PreferencesProvider.Path);
        }
    }
}