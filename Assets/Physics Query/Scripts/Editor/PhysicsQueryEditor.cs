using System.Linq;
using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private int PreviewIndex
        {
            get => Preferences.GetPreviewIndex(_query);
            set => Preferences.SetPreviewIndex(_query, value);
        }
        private Preview CurrentPreview => _availablePreviews[PreviewIndex];

        private PhysicsQuery _query;
        private Preview[] _availablePreviews;
        private string[] _previewLabels;

        private void OnEnable()
        {
            PhysicsQuery query = (PhysicsQuery)target;
            _query = query;
            _availablePreviews = Preview.CreatePreviews();
            _previewLabels = _availablePreviews.Select(x => x.Label).ToArray();
            
            for (int i = 0; i < _availablePreviews.Length; i++)
            {
                _availablePreviews[i].ElementClicked += Repaint;
            }
            _query.SetPreview(CurrentPreview.Gizmo);
        }
        private void OnDisable()
        {
            for (int i = 0; i < _availablePreviews.Length; i++)
            {
                _availablePreviews[i].ElementClicked -= Repaint;
            }
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

            _query.SetPreview(CurrentPreview.Gizmo);
        }
        private void OnSceneGUI()
        {
            CurrentPreview.DrawSceneGUI();
        }
    }
}