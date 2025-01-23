
using UnityEngine;

namespace PhysicsQuery.Editor
{
    public class Preview_Overlap : Preview
    {
        public Preview_Overlap() : base("Overlap", new GizmoPreview_Overlap(), new InspectorPreview_Overlap(), new ScenePreview_Overlap())
        {
        }

        protected override PreviewResults GetResults(PhysicsQuery query)
        {
            return new(Result<RaycastHit>.Empty, query.Overlap());
        }
    }
}