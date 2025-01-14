using UnityEditor;

namespace PhysicsQuery.Editor
{
    [CustomEditor(typeof(RayQuery))]
    public class Editor_Ray : Editor<RayQuery>
    {
        protected override PreviewForm CreatePreviewForm(RayQuery query)
        {
            return new PreviewForm_Ray(query);
        }
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
        }
    }
}