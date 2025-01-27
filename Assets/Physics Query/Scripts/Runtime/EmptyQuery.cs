using UnityEngine;

namespace PQuery
{
    public class EmptyQuery : PhysicsQuery
    {
        protected override int DoPhysicsCast(Ray worldRay, RaycastHit[] cache)
        {
            return 0;
        }
        protected override int DoPhysicsOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            return 0;
        }
    }
}
