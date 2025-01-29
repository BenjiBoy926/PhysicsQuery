using UnityEngine;
using UnityEditor;

namespace PQuery.Editor
{
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
        private string[] _previewLabels;

        private void OnEnable()
        {
            PhysicsQuery query = (PhysicsQuery)target;
            _query = query;
            _previewLabels = Preview.Labels;
            
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
            PreviewIndex = EditorGUILayout.Popup("Function", PreviewIndex, _previewLabels);
            CurrentPreview.DrawInspectorGUI(_query);
            EditorGUILayout.EndVertical();
        }
        private void OnSceneGUI()
        {
            if (Preferences.AlwaysDrawGizmos.Value)
            {
                return;
            }
            CurrentPreview.DrawSceneGUI(_query);
        }

        [InitializeOnLoadMethod]
        private static void ListenForSceneGUI()
        {
            SceneView.duringSceneGui += DrawAllSceneGUIs;
        }
        private static void DrawAllSceneGUIs(SceneView view)
        {
            if (!Preferences.AlwaysDrawGizmos.Value)
            {
                return;
            }
            PhysicsQuery[] allQueries = FindObjectsByType<PhysicsQuery>(FindObjectsSortMode.None);
            for (int i = 0; i < allQueries.Length; i++)
            {
                PhysicsQuery query = allQueries[i];
                Preview.Get(query).DrawSceneGUI(query);
            }
        }

        [InitializeOnLoadMethod]
        private static void ListenForGizmoDraw()
        {
            PhysicsQuery.DrawGizmos += OnDrawGizmos;
            PhysicsQuery.DrawGizmosSelected += OnDrawGizmosSelected;
        }
        private static void OnDrawGizmos(PhysicsQuery obj)
        {
            if (Preferences.AlwaysDrawGizmos.Value)
            {
                Preview.Get(obj).DrawGizmos(obj);
            }
        }
        private static void OnDrawGizmosSelected(PhysicsQuery obj)
        {
            if (!Preferences.AlwaysDrawGizmos.Value)
            {
                Preview.Get(obj).DrawGizmos(obj);
            }
        }
    }
}