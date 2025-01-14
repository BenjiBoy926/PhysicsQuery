using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(BoxQuery))]
    public class QueryEditor_Box : QueryEditor<BoxQuery>
    {
        protected override PreviewForm CreatePreviewForm(BoxQuery query)
        {
            return new PreviewForm_Box(query);
        }
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
        }
    }
}