using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class GizmoPreview
    {
        public abstract string Label { get; }
        public abstract void DrawGizmos(GizmoShape shape, PreviewResults results);
    }
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override string Label => "Cast";
        public override void DrawGizmos(GizmoShape shape, PreviewResults results)
        {
            shape.DrawCastGizmos(results.CastResult);
        }
    }
    public class GizmoPreview_Overlap : GizmoPreview
    {
        public override string Label => "Overlap";
        public override void DrawGizmos(GizmoShape shape, PreviewResults results)
        {
            shape.DrawOverlapGizmos(results.OverlapResult);
        }
    }
}