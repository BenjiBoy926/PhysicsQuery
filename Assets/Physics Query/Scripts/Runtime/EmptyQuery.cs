using UnityEngine;

namespace PQuery
{
    public class EmptyQuery : PhysicsQuery
    {
        protected override bool DoPhysicsCast(Ray worldRay, out RaycastHit hit)
        {
            hit = new();
            return false;
        }
        protected override int DoPhysicsCastNonAlloc(Ray worldRay, RaycastHit[] cache)
        {
            return 0;
        }
        protected override bool DoPhysicsCheck(Vector3 worldOrigin)
        {
            return false;
        }
        protected override int DoPhysicsOverlapNonAlloc(Vector3 worldOrigin, Collider[] cache)
        {
            return 0;
        }
    }
}
