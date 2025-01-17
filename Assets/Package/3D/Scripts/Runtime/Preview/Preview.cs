using UnityEngine;

namespace PhysicsQuery
{
    public abstract class Preview
    {
        public Result<RaycastHit> CastResult { get; protected set; }
        public Result<Collider> OverlapResult { get; protected set; }

        public abstract string Label { get; }
        public abstract void DrawGizmos(GizmoShape shape);
    }
    public class Preview_Cast : Preview
    {
        public override string Label => "Cast";
        public override void DrawGizmos(GizmoShape shape)
        {
            CastResult = shape.DrawCastGizmos();
        }
    }
    public class Preview_Overlap : Preview
    {
        public override string Label => "Overlap";
        public override void DrawGizmos(GizmoShape shape)
        {
            OverlapResult = shape.DrawOverlapGizmos();
        }
    }
}