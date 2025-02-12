using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsShape3D>
    {
        public override ResultSort3D GetSort(ResultSortType sortType)
        {
            return sortType == ResultSortType.None ? ResultSort3D.None : ResultSort3D.Distance;
        }
        public override MinimalRaycastHit Minimize(RaycastHit raycastHit)
        {
            return new(raycastHit);
        }

        public override float Magnitude(Vector3 vector)
        {
            return vector.magnitude;
        }
        public override Vector3 Normalize(Vector3 vector)
        {
            return vector.normalized;
        }
        public override Vector3 Subtract(Vector3 minuend, Vector3 subtrahend)
        {
            return minuend - subtrahend;
        }
        public override Vector3 TransformAsPoint(Vector3 point)
        {
            return transform.TransformPoint(point);
        }
    }
}
