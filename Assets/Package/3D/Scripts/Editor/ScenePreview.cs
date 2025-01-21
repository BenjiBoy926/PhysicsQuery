using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class ScenePreview
    {
        protected void DrawLabel(Vector3 position, int index)
        {
            Handles.Label(position, index.ToString());
        }

        public abstract void DrawSceneGUI(GizmoPreview gizmo);
    }
    public abstract class ScenePreview<TElement> : ScenePreview
    {
        public override void DrawSceneGUI(GizmoPreview gizmo)
        {
            Result<TElement> result = GetResult(gizmo);
            for (int i = 0; i < result.Count; i++)
            {
                Vector3 position = GetLabelPositionForElement(result[i]);
                DrawLabel(position, i);
            }
        }

        protected abstract Result<TElement> GetResult(GizmoPreview gizmo);
        protected abstract Vector3 GetLabelPositionForElement(TElement element);
    }

    public class ScenePreview_Cast : ScenePreview<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(GizmoPreview gizmo)
        {
            return gizmo.CastResult;
        }
        protected override Vector3 GetLabelPositionForElement(RaycastHit element)
        {
            return element.point;
        }
    }
    public class ScenePreview_Overlap : ScenePreview<Collider>
    {
        protected override Result<Collider> GetResult(GizmoPreview gizmo)
        {
            return gizmo.OverlapResult;
        }
        protected override Vector3 GetLabelPositionForElement(Collider element)
        {
            return element.transform.position;
        }
    }
}