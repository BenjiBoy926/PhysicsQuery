namespace PhysicsQuery
{
    public class Preview_Overlap : Preview
    {
        public override string Label => "Overlap";

        public Preview_Overlap(PreviewShape form) : base(form)
        {
        }

        public override void DrawGizmos()
        {
            Shape.DrawOverlapGizmos();
        }
    }
}