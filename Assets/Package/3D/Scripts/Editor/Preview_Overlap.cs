namespace PhysicsQuery.Editor
{
    public class Preview_Overlap : Preview
    {
        public override string Label => "Overlap";

        public Preview_Overlap(PreviewForm form) : base(form)
        {
        }

        public override void Draw()
        {
            Form.DrawOverlap();
        }
    }
}