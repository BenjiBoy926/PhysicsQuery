using UnityEngine;

namespace PQuery
{
    public readonly struct PhysicsParameters<TVector, TRaycastHit, TCollider>
    {
        public Matrix4x4 Space => _space;
        public LayerMask LayerMask => _layerMask;
        public QueryTriggerInteraction TriggerInteraction => _triggerInteraction;
        public TVector Origin => _origin;
        public TVector Direction => _direction;
        public float Distance => _distance;
        public TRaycastHit[] HitCache => _hitCache;
        public TCollider[] ColliderCache => _colliderCache;
        public Vector3 LossyScale => _space.lossyScale;

        private readonly Matrix4x4 _space;
        private readonly LayerMask _layerMask;
        private readonly QueryTriggerInteraction _triggerInteraction;
        private readonly TVector _origin;
        private readonly TVector _direction;
        private readonly float _distance;
        private readonly TRaycastHit[] _hitCache;
        private readonly TCollider[] _colliderCache;

        public PhysicsParameters(
            Matrix4x4 space,
            LayerMask layerMask,
            QueryTriggerInteraction triggerInteraction,
            TVector origin,
            TVector direction,
            float distance,
            TRaycastHit[] hitCache,
            TCollider[] colliderCache)
        {
            _space = space;
            _layerMask = layerMask;
            _triggerInteraction = triggerInteraction;
            _origin = origin;
            _direction = direction;
            _distance = distance;
            _hitCache = hitCache;
            _colliderCache = colliderCache;
        }

        public Vector3 TransformPoint(Vector3 point)
        {
            return Space.MultiplyPoint3x4(point);
        }
        public Vector3 TransformDirection(Vector3 direction)
        {
            return Space.MultiplyVector(direction);
        }
        public Vector3 TransformScale(Vector3 scale)
        {
            Vector3 transformedScale = Space.lossyScale;
            return new(transformedScale.x * scale.x, transformedScale.y * scale.y, transformedScale.z * scale.z);
        }

        public Vector2 TransformPoint(Vector2 point)
        {
            return Space.MultiplyPoint3x4(point);
        }
        public Vector2 TransformDirection(Vector2 direction)
        {
            return Space.MultiplyVector(direction);
        }
        public Vector2 TransformScale(Vector2 scale)
        {
            return Space.lossyScale * scale;
        }

        public Quaternion TransformRotation(Quaternion rotation)
        {
            return Space.rotation * rotation;
        }
    }
}