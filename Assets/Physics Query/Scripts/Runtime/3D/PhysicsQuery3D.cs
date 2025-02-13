using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsShape3D>
    {
        protected override ResultSort3D GetSort(ResultSortType sortType)
        {
            return sortType == ResultSortType.None ? ResultSort3D.None : ResultSort3D.Distance;
        }
        protected override MinimalRaycastHit Minimize(RaycastHit raycastHit)
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
