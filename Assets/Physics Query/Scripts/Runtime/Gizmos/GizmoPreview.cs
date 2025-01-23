using UnityEngine;

namespace PhysicsQuery
{
    public abstract class GizmoPreview
    {
        public PreviewResults Results { get; protected set; }

        public abstract string Label { get; }
        public abstract void DrawGizmos(GizmoShape shape);
    }
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override string Label => "Cast";
        public override void DrawGizmos(GizmoShape shape)
        {
            Result<RaycastHit> result = shape.DrawCastGizmos();
            Results = PreviewResults.Cast(result);
        }
    }
    public class GizmoPreview_Overlap : GizmoPreview
    {
        public override string Label => "Overlap";
        public override void DrawGizmos(GizmoShape shape)
        {
            Result<Collider> result = shape.DrawOverlapGizmos();
            Results = PreviewResults.Overlap(result);
        }
    }
}