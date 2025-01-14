using UnityEditor;
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public abstract class Preview
    {
        public abstract string Label { get; }
        protected PreviewForm Form => _form;

        private readonly PreviewForm _form;

        public Preview(PreviewForm form)
        {
            _form = form;
        }

        public abstract void Draw();
    }
}