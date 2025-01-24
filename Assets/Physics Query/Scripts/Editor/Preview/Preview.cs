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

        private void OnElementClicked(object element)
        {
            _inspector.HighlightElement(element);
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
        public void DrawGizmos(PhysicsQuery query)
        {
            _gizmo.DrawGizmos(query);
        }
        public void DrawInspectorGUI(PhysicsQuery query)
        {
            _inspector.DrawInspectorGUI(query);
        }
        public void DrawSceneGUI(PhysicsQuery query)
        {
            _scene.DrawSceneGUI(query);
        }
    }
}