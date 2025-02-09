using UnityEngine;

namespace PQuery
{
    public readonly struct PhysicsParameters<TVector, TRay, TRaycastHit, TCollider>
        where TVector : IVector<TVector>
        where TRay : IRay<TVector>
    {
        public Matrix4x4 Space => _space;
        public LayerMask LayerMask => _layerMask;
        public QueryTriggerInteraction TriggerInteraction => _triggerInteraction;
        public TVector Start => _start;
        public TVector End => _end;
        public TRaycastHit[] HitCache => _hitCache;
        public TCollider[] ColliderCache => _colliderCache;
        public Vector3 LossyScale => _space.lossyScale;

        private readonly Matrix4x4 _space;
        private readonly LayerMask _layerMask;
        private readonly QueryTriggerInteraction _triggerInteraction;
        private readonly TVector _start;
        private readonly TVector _end;
        private readonly TRaycastHit[] _hitCache;
        private readonly TCollider[] _colliderCache;

        public PhysicsParameters(
            Matrix4x4 space,
            LayerMask layerMask,
            QueryTriggerInteraction triggerInteraction,
            TVector start,
            TVector end,
            TRaycastHit[] hitCache,
            TCollider[] colliderCache)
        {
            _space = space;
            _layerMask = layerMask;
            _triggerInteraction = triggerInteraction;
            _start = start;
            _end = end;
            _hitCache = hitCache;
            _colliderCache = colliderCache;
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