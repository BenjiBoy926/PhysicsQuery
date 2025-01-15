using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(RayQuery))]
    public class QueryEditor_Ray : QueryEditor<RayQuery>
    {
        protected override PreviewShape CreatePreviewShape(RayQuery query)
        {
            return new PreviewShape_Ray(query);
        }
    }
}