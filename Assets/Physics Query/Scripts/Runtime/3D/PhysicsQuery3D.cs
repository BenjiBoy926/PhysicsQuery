using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsShape3D, AdvancedOptions3D>
    {
        protected override ResultSort3D GetNoneSort()
        {
            return ResultSort3D.None;
        }
        protected override MinimalRaycastHit MinimizeRaycastHit(RaycastHit raycastHit)
        {
            return new(raycastHit);
        }
        protected override Vector3 Wrap(Vector3 vector)
        {
            return vector;
        }
        protected override Vector3 Unwrap(Vector3 vector)
        {
            return vector;
        }
    }
}
