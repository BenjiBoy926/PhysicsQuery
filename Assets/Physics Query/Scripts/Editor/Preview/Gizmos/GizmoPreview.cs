using UnityEngine;

namespace PQuery.Editor
{
    public abstract class GizmoPreview
    {
        public abstract void DrawGizmos(PhysicsQuery query);
    }
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            GizmoShape.Get(query).DrawCastGizmos(query);
        }
    }
    public class GizmoPreview_Overlap : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            GizmoShape.Get(query).DrawOverlapGizmos(query);
        }
    }
}