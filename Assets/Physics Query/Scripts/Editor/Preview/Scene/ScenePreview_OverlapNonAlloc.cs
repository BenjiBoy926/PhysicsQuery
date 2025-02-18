using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_OverlapNonAlloc : ScenePreview_NonAlloc<Component>
    {
        protected override Result<Component> GetResult(PhysicsQuery query)
        {
            return query.AgnosticOverlapNonAlloc();
        }
        protected override string GetLabel(Component element, int index)
        {
            return $"[{index}]: {element.name}";
        }
        protected override Component GetCollider(Component element)
        {
            return element;
        }
        protected override SceneButtonStrategy<Component> GetSceneButtonStrategy()
        {
            return new SceneButtonStrategy_Collider();
        }
    }
}