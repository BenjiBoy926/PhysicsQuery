using UnityEngine;

namespace PhysicsQuery
{
    public abstract class GizmoPreview
    {
        public static GizmoPreview[] Template;
        public static int Count => Template.Length;
        public PreviewResults Results { get; protected set; }

        public abstract string Label { get; }
        public abstract void DrawGizmos(GizmoShape shape);
        public abstract GizmoPreview Clone();

        public static GizmoPreview Get(PhysicsQuery query)
        {
            return Template[Preferences.GetPreviewIndex(query)].Clone();
        }
    }
    public class GizmoPreview_Cast : GizmoPreview
    {
        public override string Label => "Cast";
        public override void DrawGizmos(GizmoShape shape)
        {
            Result<RaycastHit> result = shape.DrawCastGizmos();
            Results = PreviewResults.Cast(result);
        }
        public override GizmoPreview Clone()
        {
            return new GizmoPreview_Cast();
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
        public override GizmoPreview Clone()
        {
            return new GizmoPreview_Overlap();
        }
    }
}