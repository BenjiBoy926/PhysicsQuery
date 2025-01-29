using UnityEngine;
using UnityEditor;

namespace PQuery.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private int PreviewIndex
        {
            get => Preferences.GetPreviewIndex(_query);
            set => Preferences.SetPreviewIndex(_query, value);
        }
        private Preview CurrentPreview => Preview.Get(PreviewIndex);

        private PhysicsQuery _query;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneView.duringSceneGui += OnDuringSceneGUI;
            PhysicsQuery.DrawGizmos += OnDrawGizmos;
            PhysicsQuery.DrawGizmosSelected += OnDrawGizmosSelected;
        }

        private void OnEnable()
        {
            PhysicsQuery query = (PhysicsQuery)target;
            _query = query;
            
            for (int i = 0; i < Preview.Count; i++)
            {
                Preview.Get(i).ElementClicked += Repaint;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < Preview.Count; i++)
            {
                Preview.Get(i).ElementClicked -= Repaint;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
            PreviewIndex = EditorGUILayout.Popup("Function", PreviewIndex, Preview.Labels);
            CurrentPreview.DrawThisInspectorGUI(_query);
            EditorGUILayout.EndVertical();
        }
        private void OnSceneGUI()
        {
            Preview.DrawSceneGUI((PhysicsQuery)target);
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