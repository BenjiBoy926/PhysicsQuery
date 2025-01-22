using UnityEngine;

namespace PhysicsQuery
{
    public class RayQuery : PhysicsQuery
    {
        protected override int PerformCast(Ray worldRay, RaycastHit[] cache)
        {
            return Physics.RaycastNonAlloc(worldRay, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int PerformOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            // Note: it would be easier to invoke Cast here, but worldOrigin comes to us from the expensive use of transform.TransformPoint,
            // so we do not want to TransformPoint a second time
            Ray ray = new(worldOrigin, GetWorldDirection());
            RaycastHit[] hitCache = GetHitCache();
            int count = PerformCast(ray, hitCache);
            for (int i = 0; i < count; i++)
            {
                cache[i] = hitCache[i].collider;
            }
            return count;
        }
        protected override GizmoShape CreateGizmoShape()
        {
            return new GizmoShape_Ray(this);
        }
    }
}
