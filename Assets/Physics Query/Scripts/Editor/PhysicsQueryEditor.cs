using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

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

        private static PhysicsQueryEditor _inspector;
        private static List<PhysicsQuery> _selected = new(8);
        private static SerializedObject _serializedSelected;
        private PhysicsQuery _query;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            Selection.selectionChanged += OnSelectionChanged;
            SceneView.duringSceneGui += OnDuringSceneGUI;
            PhysicsQuery.DrawGizmos += OnDrawGizmos;
            PhysicsQuery.DrawGizmosSelected += OnDrawGizmosSelected;
            OnSelectionChanged();
        }

        private void OnEnable()
        {
            _inspector = this;
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

        private static void OnSelectionChanged()
        {
            Transform[] transforms = Selection.transforms;
            _selected.Clear();
            for (int i = 0; i < transforms.Length; i++)
            {
                Transform transform = transforms[i];
                if (transform.TryGetComponent(out PhysicsQuery query))
                {
                    _selected.Add(query);
                }
            }
            _serializedSelected = new(_selected.ToArray());
        }
        private static void OnDuringSceneGUI(SceneView view)
        {
            List<PhysicsQuery> queries;
            if (Preferences.AlwaysDrawGizmos.Value)
            {
                queries = new(FindObjectsByType<PhysicsQuery>(FindObjectsSortMode.None));
            }
            else
            {
                queries = _selected;
            }
            DrawSceneGUI(queries);
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

        private static void DrawSceneGUI(List<PhysicsQuery> queries)
        {
            for (int i = 0; i < queries.Count; i++)
            {
                Preview.DrawSceneGUI(queries[i]);
            }
        }
    }
}