namespace PhysicsQuery
{
    public class Preview_Overlap : IPreview
    {
        public string Label => "Overlap";
        public void DrawGizmos(GizmoShape shape)
        {
            shape.DrawOverlapGizmos();
        }
    }
}