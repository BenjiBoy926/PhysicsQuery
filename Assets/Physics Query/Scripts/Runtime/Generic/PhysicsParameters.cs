using UnityEngine;

namespace PQuery
{
    public struct PhysicsParameters<TVector, TRay, TRaycastHit, TCollider>
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
    {
        public Vector3 LossyScale => Space.lossyScale;

        public Matrix4x4 Space { get; private set; }
        public LayerMask LayerMask { get; private set; }
        public QueryTriggerInteraction TriggerInteraction { get; private set; }
        public TVector Start { get; private set; }
        public TVector End { get; private set; }
        public TRaycastHit[] HitCache { get; private set; }
        public TCollider[] ColliderCache { get; private set; }

        public PhysicsParameters(
            Matrix4x4 space,
            LayerMask layerMask,
            QueryTriggerInteraction triggerInteraction,
            TVector start,
            TVector end,
            TRaycastHit[] hitCache,
            TCollider[] colliderCache)
        {
            Space = space;
            LayerMask = layerMask;
            TriggerInteraction = triggerInteraction;
            Start = start;
            End = end;
            HitCache = hitCache;
            ColliderCache = colliderCache;
        }

        public RayDistance<TVector, TRay> GetWorldRay()
        {
            return new(GetWorldStart(), GetWorldEnd());
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