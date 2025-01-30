namespace PQuery.Editor
{
    public class GizmoPreview_OverlapNonAlloc : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            DrawOverlapNonAllocGizmos(query);
        }
    }
}