using UnityEditor;
using UnityEngine;

namespace PQuery.Editor
{
    public class ScenePreview_CastNonAlloc : ScenePreview_NonAlloc<RaycastHit>
    {
        protected override SceneButtonStrategy GetButtonStrategy()
        {
            return new SceneButtonStrategy_RaycastHit();
        }
        protected override Result<RaycastHit> GetResult(PhysicsQuery query)
        {
            return query.CastNonAlloc(ResultSort.Distance);
        }
    }
}