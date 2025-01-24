using System;
using System.Linq;
using UnityEditor;

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

        public static void DrawGizmos(PhysicsQuery query)
        {
            int previewIndex = Preferences.GetPreviewIndex(query);
            Preview preview = Get(previewIndex);
            preview.DrawGizmosInternal(query);
        }
        public static Preview Get(PhysicsQuery query)
        {
            int index = Preferences.GetPreviewIndex(query);
            return Get(index);
        }
        public static Preview Get(int index)
        {
            return _previews[index];
        }

        private void DrawGizmosInternal(PhysicsQuery query)
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