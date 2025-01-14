using UnityEngine;
using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(PhysicsQuery), true)]
    public class PhysicsQueryEditor : UnityEditor.Editor
    {
        private const float MaxDistance = 1000;
        private const float NormalLength = 0.1f;

        private int CurrentPreview
        {
            get => EditorPrefs.GetInt(GetPreviewPrefKey(), 0);
            set => EditorPrefs.SetInt(GetPreviewPrefKey(), value);
        }

        private PhysicsQuery _query;
        private readonly string[] _previewNames = new string[]
        {
            "Cast", "CastAll"
        };

        private void OnEnable()
        {
            _query = (PhysicsQuery)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
                CurrentPreview = EditorGUILayout.Popup("Preview", CurrentPreview, _previewNames);
            }
        }
        private void OnSceneGUI()
        {
            switch (CurrentPreview)
            {
                case 0:
                    bool didHit = _query.Cast(out RaycastHit hit);
                    if (didHit)
                    {
                        DrawHit(hit);
                    }
                    else
                    {
                        DrawNoHit();
                    }
                    break;
                case 1:
                    RaycastHit[] hits = _query.CastAll();
                    if (hits.Length > 0)
                    {
                        DrawHits(hits);
                    }
                    else
                    {
                        DrawNoHit();
                    }
                    break;
            }
        }
        private void DrawHits(RaycastHit[] hits)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                DrawHit(hits[i]);
            }
        }
        private void DrawHit(RaycastHit hit)
        {
            Ray worldRay = _query.GetWorldRay();
            Handles.color = Color.green;
            Handles.DrawLine(worldRay.origin, hit.point);

            Ray normal = new(hit.point, hit.normal);
            Handles.color = Color.red;
            Handles.DrawLine(normal.origin, normal.GetPoint(NormalLength));
        }
        private void DrawNoHit()
        {
            Ray worldRay = _query.GetWorldRay();
            Vector3 start = worldRay.origin;
            Vector3 end = worldRay.GetPoint(GetMaxDistance());
            Handles.color = Color.gray;
            Handles.DrawLine(start, end); 
        }
        private float GetMaxDistance()
        {
            return Mathf.Min(MaxDistance, _query.MaxDistance);
        }
        private string GetPreviewPrefKey()
        {
            return $"Preview:{_query.GetInstanceID()}";
        }
    }
}