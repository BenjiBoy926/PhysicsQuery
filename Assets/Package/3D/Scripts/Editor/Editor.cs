using UnityEditor;
using System.Linq;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class Editor<TQuery> : UnityEditor.Editor where TQuery : PhysicsQuery
    {
        private int CurrentPreviewIndex
        {
            get => ValidatePreviewIndex(GetPreviewPrefValue());
            set => EditorPrefs.SetInt(GetPreviewPrefKey(), ValidatePreviewIndex(value));
        }

        private TQuery _query;
        private PreviewForm _form;
        private Preview[] _previews;
        private string[] _previewLabels;

        private void OnEnable()
        {
            _query = (TQuery)target;
            _form = CreatePreviewForm(_query);
            _previews = new Preview[]
            {
                new Preview_Cast(_form),
                new Preview_Overlap(_form)
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
                CurrentPreviewIndex = EditorGUILayout.Popup("Function", CurrentPreviewIndex, _previewLabels);
            }
        }
        protected virtual void OnSceneGUI()
        {
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
            return $"PreviewFunction:{_query.GetInstanceID()}";
        }

        protected abstract PreviewForm CreatePreviewForm(TQuery query);
    }
}