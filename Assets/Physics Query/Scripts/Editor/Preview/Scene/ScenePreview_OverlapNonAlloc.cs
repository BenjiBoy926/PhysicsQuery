using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_OverlapNonAlloc : ScenePreview_NonAlloc<Collider>
    {
        protected override SceneButtonStrategy GetButtonStrategy()
        {
            return new SceneButtonStrategy_Collider();
        }
        protected override Result<Collider> GetResult(PhysicsQuery query)
        {
            return query.OverlapNonAlloc();
        }
        protected override string GetLabel(Collider element, int index)
        {
            return $"[{index}]: {element.name}";
        }
    }
}