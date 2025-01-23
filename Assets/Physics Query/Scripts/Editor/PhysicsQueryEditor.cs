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
            for (int i = 0; i < Preview.Count; i++)
            {
                Preview.Get(i).ElementClicked += Repaint;
            }
            _query.DrawGizmos += OnDrawGizmos;
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
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
                CurrentPreviewIndex = EditorGUILayout.Popup("Function", CurrentPreviewIndex, _previewLabels);
                CurrentPreview.DrawInspectorGUI();
            }
        }
        private void OnSceneGUI()
        {
            CurrentPreview.DrawSceneGUI();
        }
        private void OnDrawGizmos()
        {
            CurrentPreview.DrawGizmos(_query);
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