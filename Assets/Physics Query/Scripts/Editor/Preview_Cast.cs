namespace PhysicsQuery.Editor
{
    public class Preview_Cast : Preview
    {
        public Preview_Cast() : base("Cast", new GizmoPreview_Cast(), new InspectorPreview_Cast(), new ScenePreview_Cast())
        {
        }
    }
}