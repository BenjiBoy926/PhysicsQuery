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
        private GizmoMode CurrentPreview => _modes[CurrentPreviewIndex];
        private ResultDisplay CurrentDisplay => _displays[CurrentPreviewIndex];

        private PhysicsQuery _query;
        private readonly GizmoMode[] _modes = new GizmoMode[] 
        {
            new GizmoMode_Cast(),
            new GizmoMode_Overlap()
        };
        private readonly ResultDisplay[] _displays = new ResultDisplay[]
        {
            new ResultDisplay_Cast(),
            new ResultDisplay_Overlap()
        };
        private string[] _previewLabels;

        private void OnEnable()
        {
            _query = (PhysicsQuery)target;
            _previewLabels = _modes.Select(x => x.Label).ToArray();
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
                CurrentDisplay.DrawInspectorGUI(CurrentPreview);
            }
        }

        private void SetPreview()
        {
            _query.SetGizmoMode(CurrentPreview);
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