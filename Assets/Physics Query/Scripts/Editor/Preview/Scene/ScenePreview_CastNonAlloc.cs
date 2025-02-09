using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_CastNonAlloc : ScenePreview_NonAlloc<RaycastHit>
    {
        protected override Result<RaycastHit> GetResult(PhysicsQuery3D query)
        {
            return query.CastNonAlloc(ResultSort3D.Distance);
        }
        protected override string GetLabel(RaycastHit element, int index)
        {
            return $"[{index}]: {element.collider.name}";
        }
        protected override Collider GetCollider(RaycastHit element)
        {
            return element.collider;
        }
        protected override SceneButtonStrategy<RaycastHit> GetSceneButtonStrategy()
        {
            return new SceneButtonStrategy_RaycastHit();
        }
    }
}