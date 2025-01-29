using System;
using System.Linq;
using UnityEditor;

namespace PQuery.Editor
{
    public class Preview
    {
        public event Action ElementClicked = delegate { };

        public static string[] Labels => _previews.Select(x => x.Label).ToArray();
        public static int Count => _previews.Length;
        public string Label => ObjectNames.NicifyVariableName(_methodName);

        private static readonly Preview[] _previews = new Preview[]
        {
            new(nameof(PhysicsQuery.Cast), new GizmoPreview_Cast(), new InspectorPreview_Cast(), new ScenePreview_Cast()),
            new(nameof(PhysicsQuery.CastNonAlloc), new GizmoPreview_CastNonAlloc(), new InspectorPreview_CastNonAlloc(), new ScenePreview_CastNonAlloc()),
            new(nameof(PhysicsQuery.Check), new GizmoPreview_Check(), new InspectorPreview_Check(), new ScenePreview_Check()),
            new(nameof(PhysicsQuery.OverlapNonAlloc), new GizmoPreview_OverlapNonAlloc(), new InspectorPreview_OverlapNonAlloc(), new ScenePreview_OverlapNonAlloc()),
        };
        private readonly string _methodName;
        private readonly GizmoPreview _gizmo;
        private readonly InspectorPreview _inspector;
        private readonly ScenePreview _scene;

        protected Preview(string methodName, GizmoPreview gizmo, InspectorPreview inspector, ScenePreview scene)
        {
            _methodName = methodName;
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

        public static Preview Get(PhysicsQuery query)
        {
            int index = Preferences.GetPreviewIndex(query);
            return Get(index);
        }
        public static Preview Get(int index)
        {
            return _previews[index];
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