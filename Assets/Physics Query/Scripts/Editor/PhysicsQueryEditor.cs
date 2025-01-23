using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private int PreviewIndex
        {
            get => ClampPreviewIndex(_previewIndex.Value);
            set => _previewIndex.Value = ClampPreviewIndex(value);
        }
        private Preview CurrentPreview => _availablePreviews[PreviewIndex];

        private PhysicsQuery _query;
        private PreferenceProperty<int> _previewIndex;
        private Preview[] _availablePreviews;
        private string[] _previewLabels;

        private void OnEnable()
        {
            PhysicsQuery query = (PhysicsQuery)target;
            _query = query;
            _previewIndex = new PreferenceProperty<int>($"PreviewFunction{query.GetInstanceID()}", 0);
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

        private int ClampPreviewIndex(int index)
        {
            return Mathf.Clamp(index, 0, _availablePreviews.Length);
        }
    }
}