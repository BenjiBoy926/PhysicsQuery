using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(CapsuleQuery))]
    public class QueryEditor_Capsule : QueryEditor<CapsuleQuery>
    {
        protected override PreviewShape CreatePreviewShape(CapsuleQuery query)
        {
            return new PreviewShape_Capsule(query);
        }
    }
}