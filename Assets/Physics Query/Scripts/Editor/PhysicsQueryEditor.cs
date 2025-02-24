using UnityEngine;
using UnityEditor;

namespace PQuery.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {            
            ScenePreview.ColliderClicked += OnColliderClicked;
        }
        private void OnDisable()
        {
            ScenePreview.ColliderClicked -= OnColliderClicked;
        }

        private void OnColliderClicked(Collider other)
        {
            Repaint();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Object[] targets = serializedObject.targetObjects;
            for (int i = 0; i < targets.Length; i++)
            {
                DrawInspectorPreview((PhysicsQuery)targets[i]);
            }
        }
        private void OnSceneGUI()
        {
            Preview.DrawSceneGUI((PhysicsQuery)target);
        }

        private void DrawInspectorPreview(PhysicsQuery query)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField($"'{query.name}' Preview", EditorStyles.boldLabel);
            int current = Preferences.GetPreviewIndex(query);
            int next = EditorGUILayout.Popup("Function", current, Preview.Labels);
            if (current != next)
            {
                Preferences.SetPreviewIndex(query, next);
            }
            Preview.DrawInspectorGUI(query);
            EditorGUILayout.EndVertical();
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneView.duringSceneGui += OnDuringSceneGUI;
            PhysicsQuery.DrawGizmos += OnDrawGizmos;
            PhysicsQuery.DrawGizmosSelected += OnDrawGizmosSelected;
        }
        private static void OnDuringSceneGUI(SceneView view)
        {
            if (!Preferences.AlwaysDrawGizmos.Value)
            {
                return;
            }
            PhysicsQuery[] queries = FindObjectsByType<PhysicsQuery>(FindObjectsSortMode.None);
            for (int i = 0; i < queries.Length; i++)
            {
                Preview.DrawSceneGUI(queries[i]);
            }
        }
        private static void OnDrawGizmos(PhysicsQuery obj)
        {
            if (Preferences.AlwaysDrawGizmos.Value)
            {
                Preview.DrawGizmos(obj);
            }
        }
        private static void OnDrawGizmosSelected(PhysicsQuery obj)
        {
            if (!Preferences.AlwaysDrawGizmos.Value)
            {
                Preview.DrawGizmos(obj);
            }
        }
    }
}