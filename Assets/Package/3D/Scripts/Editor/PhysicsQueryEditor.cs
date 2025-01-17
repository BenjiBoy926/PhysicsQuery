using UnityEditor;
using System.Linq;
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

        private PhysicsQuery _query;
        private Preview[] _previews;
        private string[] _previewLabels;

        private void OnEnable()
        {
            _query = (PhysicsQuery)target;
            _previews = new Preview[]
            {
                new Preview_Cast(),
                new Preview_Overlap()
            };
            _previewLabels = _previews.Select(x => x.Label).ToArray();
            SetPreview();
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
                CurrentPreviewIndex = EditorGUILayout.Popup("Function", CurrentPreviewIndex, _previewLabels);
                SetPreview();
            }
        }

        private void SetPreview()
        {
            _query.SetPreview(_previews[CurrentPreviewIndex]);
        }
        private int ValidatePreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, _previewLabels.Length - 1);
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