namespace PhysicsQuery
{
    public class Preview_Overlap : Preview
    {
        public override string Label => "Overlap";

        public Preview_Overlap(PreviewForm form) : base(form)
        {
        }

        public override void DrawGizmos()
        {
            Form.DrawOverlapGizmos();
        }
    }
}