using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(BoxQuery))]
    public class QueryEditor_Box : QueryEditor<BoxQuery>
    {
        protected override PreviewShape CreatePreviewShape(BoxQuery query)
        {
            return new PreviewShape_Box(query);
        }
    }
}