using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoPreview
    {
        public abstract void DrawGizmos(PhysicsQuery query);
    }
    public class GizmoPreview_CastNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            GizmoShape.Get(query).DrawCastNonAllocGizmos(query);
        }
    }
    public class GizmoPreview_OverlapNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            GizmoShape.Get(query).DrawOverlapNonAllocGizmos(query);
        }
    }
}