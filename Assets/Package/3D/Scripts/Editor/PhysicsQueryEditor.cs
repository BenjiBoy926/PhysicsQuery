using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private int CurrentPreviewIndex
        {
            get => ValidatePreviewIndex(GetPreviewPrefValue());
            set => EditorPrefs.SetInt(GetPreviewPrefKey(), ValidatePreviewIndex(value));
        }
        private Preview CurrentPreview => Preview.Get(CurrentPreviewIndex);

        private PhysicsQuery _query;
        private string[] _previewLabels;

        private void OnEnable()
        {
            _query = (PhysicsQuery)target;
            _previewLabels = Preview.Labels;
            CurrentPreview.SetGizmoModeOn(_query);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
                CurrentPreviewIndex = EditorGUILayout.Popup("Function", CurrentPreviewIndex, _previewLabels);
                CurrentPreview.SetGizmoModeOn(_query);
                CurrentPreview.DrawInspectorGUI();
            }
        }
        private void OnSceneGUI()
        {
            CurrentPreview.DrawSceneGUI();
        }

        private int ValidatePreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, Preview.Count - 1);
        }
        private int GetPreviewPrefValue()
        {
            return EditorPrefs.GetInt(GetPreviewPrefKey(), 0);
        }
        private string GetPreviewPrefKey()
        {
            return $"PreviewFunction:{_query.GetInstanceID()}";
        }
    }
}