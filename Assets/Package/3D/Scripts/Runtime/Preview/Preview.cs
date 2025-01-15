namespace PhysicsQuery
{
    public abstract class Preview
    {
        public abstract string Label { get; }
        protected PreviewShape Shape => _shape;

        private readonly PreviewShape _shape;

        public Preview(PreviewShape form)
        {
            _shape = form;
        }

        public abstract void DrawGizmos();
    }
}