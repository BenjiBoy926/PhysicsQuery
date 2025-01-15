using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(SphereQuery))]
    public class QueryEditor_Sphere : QueryEditor<SphereQuery>
    {
        protected override PreviewShape CreatePreviewShape(SphereQuery query)
        {
            return new PreviewShape_Sphere(query);
        }
    }
}