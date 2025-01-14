using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(RayQuery))]
    public class QueryEditor_Ray : QueryEditor<RayQuery>
    {
        protected override PreviewForm CreatePreviewForm(RayQuery query)
        {
            return new PreviewForm_Ray(query);
        }
    }
}