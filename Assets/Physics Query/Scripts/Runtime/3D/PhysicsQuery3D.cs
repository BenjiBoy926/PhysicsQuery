using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsQuery3D.Shape, AdvancedOptions3D>
    {
        protected override Shape GetDefaultShape()
        {
            return new RayShape();
        }
        protected override AdvancedOptions3D GetDefaultOptions()
        {
            return AdvancedOptions3D.Default;
        }
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
