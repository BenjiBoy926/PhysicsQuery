namespace PhysicsQuery.Editor
{
    public class Preview_Overlap : Preview
    {
        public Preview_Overlap() : base("Overlap", new GizmoPreview_Overlap(), new InspectorPreview_Overlap(), new ScenePreview_Overlap())
        {
        }
    }
}