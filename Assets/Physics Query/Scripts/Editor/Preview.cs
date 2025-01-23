using System;
using System.Linq;

namespace PhysicsQuery.Editor
{
    public abstract class Preview
    {
        public event Action ElementClicked = delegate { };

        public static string[] Labels => _previews.Select(x => x.Label).ToArray();
        public static int Count => _previews.Length;
        public string Label => _label;

        private static readonly Preview[] _previews = new Preview[]
        {
            new Preview_Cast(),
            new Preview_Overlap()
        };

        private readonly string _label;
        private readonly GizmoPreview _gizmo;
        private readonly InspectorPreview _inspector;
        private readonly ScenePreview _scene;
        private PreviewResults _results;

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
            _inspector.HighlightElement(_results, index);
            ElementClicked();
        }

        public static Preview[] CreatePreviews()
        {
            return new Preview[] { new Preview_Cast(), new Preview_Overlap() };
        }
        public static Preview Get(int i)
        {
            return _previews[i];
        }
        public void Update(PhysicsQuery query)
        {
            _results = GetResults(query);
        }
        public void DrawInspectorGUI()
        {
            _inspector.DrawInspectorGUI(_results);
        }
        public void DrawSceneGUI()
        {
            _scene.DrawSceneGUI(_results);
        }
        public void DrawGizmos(PhysicsQuery query)
        {
            _gizmo.DrawGizmos(query.GizmoShape, _results);
        }

        protected abstract PreviewResults GetResults(PhysicsQuery query);
    }
}