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
                new Preview_Cast(_query),
                new Preview_CastNonAlloc(_query),
            };
            _previewLabels = _previews.Select(x => x.Label).ToArray();
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
                CurrentPreviewIndex = EditorGUILayout.Popup("Preview", CurrentPreviewIndex, _previewLabels);
            }
        }
        private void OnSceneGUI()
        {
            if (_query is BoxQuery boxQuery)
            {
                Matrix4x4 oldMatrix = Handles.matrix;
                Handles.matrix = Matrix4x4.Rotate(boxQuery.GetWorldOrientation());
                Handles.DrawWireCube(_query.GetWorldOrigin(), boxQuery.GetWorldExtents() * 2);
                Handles.matrix = oldMatrix;
            }
            DrawPreview();
        }
        private void DrawPreview()
        {
            _previews[CurrentPreviewIndex].Draw();
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
            return $"Preview:{_query.GetInstanceID()}";
        }
    }
}