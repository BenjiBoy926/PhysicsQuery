using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private const string PrefKeyPrefix = "PreviewFunction:";

        private string PrefKey => $"{PrefKeyPrefix}{_queryID}";
        private int PreviewIndex
        {
            get => GetPreviewIndex();
            set => SetPreviewIndex(value);
        }
        private Preview CurrentPreview => _availablePreviews[PreviewIndex];

        private PhysicsQuery _query;
        private int _queryID;
        private Preview[] _availablePreviews;
        private string[] _previewLabels;

        private void OnEnable()
        {
            PhysicsQuery query = (PhysicsQuery)target;
            _query = query;
            _queryID = query.GetInstanceID();
            _availablePreviews = Preview.CreatePreviews();
            _previewLabels = _availablePreviews.Select(x => x.Label).ToArray();
            
            _query.DrawGizmos += OnDrawGizmos;
            for (int i = 0; i < _availablePreviews.Length; i++)
            {
                _availablePreviews[i].ElementClicked += Repaint;
            }
        }
        private void OnDisable()
        {
            _query.DrawGizmos -= OnDrawGizmos;
            for (int i = 0; i < _availablePreviews.Length; i++)
            {
                _availablePreviews[i].ElementClicked -= Repaint;
            }
        }

        private void OnDrawGizmos()
        {
            CurrentPreview.Update(_query);
            CurrentPreview.DrawGizmos(_query);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
            PreviewIndex = EditorGUILayout.Popup("Function", PreviewIndex, _previewLabels);
            CurrentPreview.DrawInspectorGUI();
            EditorGUILayout.EndVertical();
        }
        private void OnSceneGUI()
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