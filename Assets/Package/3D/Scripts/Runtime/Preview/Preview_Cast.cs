namespace PhysicsQuery
{
    public class Preview_Cast : IPreview
    {
        public string Label => "Cast";
        public void DrawGizmos(GizmoShape shape)
        {
            shape.DrawCastGizmos();
        }
    }
}