using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_CastNonAlloc : ScenePreview_NonAlloc<AgnosticRaycastHit>
    {
        protected override Result<AgnosticRaycastHit> GetResult(PhysicsQuery query)
        {
            return query.AgnosticCastNonAlloc(ResultSortAgnostic.Distance);
        }
        protected override string GetLabel(AgnosticRaycastHit element, int index)
        {
            return $"[{index}]: {element.Collider.name}";
        }
        protected override Component GetCollider(AgnosticRaycastHit element)
        {
            return element.Collider;
        }
        protected override SceneButtonStrategy<AgnosticRaycastHit> GetSceneButtonStrategy()
        {
            return new SceneButtonStrategy_RaycastHit();
        }
    }
}