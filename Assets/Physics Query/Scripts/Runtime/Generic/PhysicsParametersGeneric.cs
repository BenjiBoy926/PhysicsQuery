using UnityEngine;

namespace PQuery
{
    public class PhysicsParametersGeneric<TVector, TRay, TRayDistance, TRaycastHit, TCollider>
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
        where TRayDistance : RayDistanceGeneric<TVector, TRay>, new()
    {
        public Vector3 LossyScale => Space.lossyScale;

        public Matrix4x4 Space { get; internal set; }
        public LayerMask LayerMask { get; internal set; }
        public QueryTriggerInteraction TriggerInteraction { get; internal set; }
        public TVector Start { get; internal set; }
        public TVector End { get; internal set; }
        public TRaycastHit[] HitCache { get; internal set; }
        public TCollider[] ColliderCache { get; internal set; }

        private readonly TRayDistance _rayDistance = new();

        public TRayDistance GetWorldRay()
        {
            _rayDistance.SetStartAndEnd(GetWorldStart(), GetWorldEnd());
            return _rayDistance;
        }
        public TVector GetWorldStart()
        {
            return TransformPoint(Start);
        }
        public TVector GetWorldEnd()
        {
            return TransformPoint(End);
        }
        public TVector TransformPoint(TVector point)
        {
            return point.TransformAsPoint(Space);
        }
        public TVector TransformDirection(TVector direction)
        {
            return direction.TransformAsDirection(Space);
        }
        public Quaternion TransformRotation(Quaternion rotation)
        {
            return Space.rotation * rotation;
        }
        public TVector TransformScale(TVector scale)
        {
            return scale.TransformAsScale(Space);
        }
    }
}