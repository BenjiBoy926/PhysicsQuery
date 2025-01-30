namespace PQuery.Editor
{
    public class GizmoPreview_CastNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            DrawCastNonAllocGizmos(query);
        }
    }
}