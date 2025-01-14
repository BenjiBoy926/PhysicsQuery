using UnityEngine;
using UnityEditor;
using System.Linq;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private int CurrentPreview
        {
            get => EditorPrefs.GetInt(GetPreviewPrefKey(), 0);
            set => EditorPrefs.SetInt(GetPreviewPrefKey(), value);
        }

        private PhysicsQuery _query;
        private PhysicsQueryPreview[] _previews;
        private string[] _previewLabels;

        private void OnEnable()
        {
            _query = (PhysicsQuery)target;
            _previews = new PhysicsQueryPreview[]
            {
                new PhysicsQueryPreview_Cast(_query),
                new PhysicsQueryPreview_CastAll(_query),
                new PhysicsQueryPreview_CastNonAlloc(_query),
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
                CurrentPreview = EditorGUILayout.Popup("Preview", CurrentPreview, _previewLabels);
            }
        }
        private void OnSceneGUI()
        {
            DrawPreview();
        }
        private void DrawPreview()
        {
            _previews[CurrentPreview].Draw();
        }

        private string GetPreviewPrefKey()
        {
            return $"Preview:{_query.GetInstanceID()}";
        }
    }
}