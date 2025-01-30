namespace PQuery.Editor
{
    public class GizmoPreview_Check : GizmoPreview
    {
        public override void DrawGizmos(PhysicsQuery query)
        {
            DrawCheckGizmos(query);
        }
    }
}