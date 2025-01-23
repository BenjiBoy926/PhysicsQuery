using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class PhysicsQueryDisplay
    {
        private const string PrefKeyPrefix = "PreviewFunction:";

        private string PrefKey => $"{PrefKeyPrefix}{_queryID}";
        private int PreviewIndex
        {
            get => GetPreviewIndex();
            set => SetPreviewIndex(value);
        }
        private Preview CurrentPreview => _availablePreviews[PreviewIndex];

        private readonly PhysicsQuery _query;
        private readonly int _queryID;
        private readonly Preview[] _availablePreviews;
        private readonly string[] _previewLabels;

        public PhysicsQueryDisplay(PhysicsQuery query)
        {
            _query = query;
            _queryID = query.GetInstanceID();
            _availablePreviews = Preview.CreatePreviews();
            _previewLabels = _availablePreviews.Select(x => x.Label).ToArray();
            query.DrawGizmos += OnDrawGizmos;
        }
        
        private void OnDrawGizmos()
        {
            CurrentPreview.Update(_query);
            CurrentPreview.DrawGizmos(_query);
        }
        public void DrawInspectorGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
            PreviewIndex = EditorGUILayout.Popup("Function", PreviewIndex, _previewLabels);
            CurrentPreview.DrawInspectorGUI();
            EditorGUILayout.EndVertical();
        }
        public void DrawSceneGUI()
        {
            CurrentPreview.DrawSceneGUI();
        }

        private int GetPreviewIndex()
        {
            int storedValue = EditorPrefs.GetInt(PrefKeyPrefix, 0);
            return ClampPreviewIndex(storedValue);
        }
        private void SetPreviewIndex(int index)
        {
            index = ClampPreviewIndex(index);
            EditorPrefs.SetInt(PrefKey, index);
        }
        private int ClampPreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, _availablePreviews.Length);
        }
    }
}