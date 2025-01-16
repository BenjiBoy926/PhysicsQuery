namespace PhysicsQuery
{
    public class Preview_Overlap : IPreview
    {
        public string Label => "Overlap";
        public void DrawGizmos(PreviewShape shape)
        {
            shape.DrawOverlapGizmos();
        }
    }
}