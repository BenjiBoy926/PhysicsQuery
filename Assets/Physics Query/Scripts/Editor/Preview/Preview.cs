using System;
using UnityEditor;

namespace PhysicsQuery.Editor
{
    public class Preview
    {
        public event Action ElementClicked = delegate { };

        public string Label => _label;
        public GizmoPreview Gizmo => _gizmo;

        private readonly string _label;
        private readonly GizmoPreview _gizmo;
        private readonly InspectorPreview _inspector;
        private readonly ScenePreview _scene;

        protected Preview(string label, GizmoPreview gizmo, InspectorPreview inspector, ScenePreview scene)
        {
            _label = label;
            _gizmo = gizmo;
            _inspector = inspector;
            _scene = scene;
            scene.ElementClicked += OnElementClicked;
        }

        private void OnElementClicked(int index)
        {
            _inspector.HighlightElement(_gizmo.Results, index);
            ElementClicked();
        }

        public static Preview[] CreatePreviews()
        {
            return new Preview[] 
            { 
                new("Cast", new GizmoPreview_Cast(), new InspectorPreview_Cast(), new ScenePreview_Cast()), 
                new("Overlap", new GizmoPreview_Overlap(), new InspectorPreview_Overlap(), new ScenePreview_Overlap()),
            };
        }
        public void DrawInspectorGUI()
        {
            _inspector.DrawInspectorGUI(_gizmo.Results);
        }
        public void DrawSceneGUI()
        {
            _scene.DrawSceneGUI(_gizmo.Results);
        }

        // Note: somewhat of a hack. We want "CreatePreviews" to be the source of truth on the number and order of previews,
        // but we need the order and number of gizmo previews to be accessible from non-editor code. So we set a public variable
        // from this initialize on load method
        [InitializeOnLoadMethod]
        private static void SetupGizmoPreviews()
        {
            Preview[] previews = CreatePreviews();
            GizmoPreview[] gizmos = new GizmoPreview[previews.Length];
            for (int i = 0; i < gizmos.Length; i++)
            {
                gizmos[i] = previews[i].Gizmo;
            }
            GizmoPreview.Template = gizmos;
        }
    }
}