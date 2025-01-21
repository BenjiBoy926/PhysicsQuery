using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class ScenePreview
    {
        public abstract void DrawSceneGUI(GizmoPreview gizmo);
    }
    public abstract class ScenePreview<TElement> : ScenePreview
    {
        public override void DrawSceneGUI(GizmoPreview gizmo)
        {
            Result<TElement> result = GetResult(gizmo);
            for (int i = 0; i < result.Count; i++)
            {
                DrawSceneGUIForElement(result[i], i);
            }
        }

        protected abstract Result<TElement> GetResult(GizmoPreview gizmo);
        protected abstract void DrawSceneGUIForElement(TElement element, int index);
    }

    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(GizmoPreview gizmo)
        {
            return gizmo.CastResult;
        }
        protected override void DrawSceneGUIForElement(RaycastHit element, int index)
        {
            Handles.Label(element.point, index.ToString());
        }
    }
    public class ScenePreview_Overlap : ScenePreview<Collider>
    {
        protected override Result<Collider> GetResult(GizmoPreview gizmo)
        {
            return gizmo.OverlapResult;
        }
        protected override void DrawSceneGUIForElement(Collider element, int index)
        {
            Handles.Label(element.transform.position, index.ToString());
        }
    }
}