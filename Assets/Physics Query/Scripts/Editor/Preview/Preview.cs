using System.Linq;
using UnityEditor;

namespace PQuery.Editor
{
    public class Preview
    {
        public static string[] Labels => _labels ??= CreateLabels();
        public static int Count => _previews.Length;
        public string Label => ObjectNames.NicifyVariableName(_methodName);

        private static readonly Preview[] _previews = new Preview[]
        {
            new(nameof(PhysicsQuery3D.Cast), new GizmoPreview_Cast(), new InspectorPreview_Cast(), new ScenePreview_Cast()),
            new(nameof(PhysicsQuery3D.CastNonAlloc), new GizmoPreview_CastNonAlloc(), new InspectorPreview_CastNonAlloc(), new ScenePreview_CastNonAlloc()),
            new(nameof(PhysicsQuery3D.Check), new GizmoPreview_Check(), new InspectorPreview_Check(), new ScenePreview_Check()),
            new(nameof(PhysicsQuery3D.OverlapNonAlloc), new GizmoPreview_OverlapNonAlloc(), new InspectorPreview_OverlapNonAlloc(), new ScenePreview_OverlapNonAlloc()),
        };
        private static string[] _labels;

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
        }

        public static void DrawGizmos(PhysicsQuery query)
        {
            Get(query).DrawThisGizmo(query);
        }
        public static void DrawInspectorGUI(PhysicsQuery query)
        {
            Get(query).DrawThisInspectorGUI(query);
        }
        public static void DrawSceneGUI(PhysicsQuery query)
        {
            Get(query).DrawThisSceneGUI(query);
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
        private static string[] CreateLabels()
        {
            return _previews.Select(x => x.Label).ToArray();
        }

        public void DrawThisGizmo(PhysicsQuery query)
        {
            _gizmo.DrawGizmos(query);
        }
        public void DrawThisInspectorGUI(PhysicsQuery query)
        {
            _inspector.DrawInspectorGUI(query);
        }
        public void DrawThisSceneGUI(PhysicsQuery query)
        {
            _scene.DrawSceneGUI(query);
        }
    }
}