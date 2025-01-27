using UnityEngine;

namespace PQuery
{
    public class EmptyQuery : PhysicsQuery
    {
        protected override int PerformCast(Ray worldRay, RaycastHit[] cache)
        {
            return 0;
        }
        protected override int PerformOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            return 0;
        }
    }
}
