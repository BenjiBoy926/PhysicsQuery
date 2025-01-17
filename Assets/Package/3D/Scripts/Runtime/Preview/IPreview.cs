namespace PhysicsQuery
{
    public interface IPreview
    {
        string Label { get; }
        void DrawGizmos(GizmoShape shape);
    }
}