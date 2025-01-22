using System;
using System.Linq;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class Preview
    {
        public event Action ElementClicked = delegate { };

        public static string[] Labels => _previews.Select(x => x.Label).ToArray();
        public static int Count => _previews.Length;
        public string Label => _label;

        private static readonly Preview[] _previews = new Preview[]
        {
            new("Cast", new GizmoPreview_Cast(), new InspectorPreview_Cast(), new ScenePreview_Cast()),
            new("Overlap", new GizmoPreview_Overlap(), new InspectorPreview_Overlap(), new ScenePreview_Overlap()),
        };

        private readonly string _label;
        private readonly GizmoPreview _gizmo;
        private readonly InspectorPreview _inspector;
        private readonly ScenePreview _scene;

        private Preview(string label, GizmoPreview mode, InspectorPreview display, ScenePreview scene)
        {
            _label = label;
            _gizmo = mode;
            _inspector = display;
            _scene = scene;
            scene.ElementClicked += OnElementClicked;
        }

        private void OnElementClicked(int index)
        {
            _inspector.HighlightElement(_gizmo, index);
            ElementClicked();
        }

        public static Preview Get(int i)
        {
            return _previews[i];
        }
        public void SetGizmoPreviewOn(PhysicsQuery query)
        {
            query.SetGizmoPreview(_gizmo);
        }
        public void DrawInspectorGUI()
        {
            _inspector.DrawInspectorGUI(_gizmo);
        }
        public void DrawSceneGUI()
        {
            _scene.DrawSceneGUI(_gizmo);
        }
    }
}