namespace PhysicsQuery
{
    public interface IPreview
    {
        string Label { get; }
        void DrawGizmos(PreviewShape shape);
    }
}