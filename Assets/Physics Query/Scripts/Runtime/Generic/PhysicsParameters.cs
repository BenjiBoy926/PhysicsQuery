using UnityEngine;

namespace PQuery
{
    public readonly struct PhysicsParameters<TVector, TRaycastHit, TCollider, TAdvancedOptions>
    {
        public Matrix4x4 Space => _space;
        public TVector Origin => _origin;
        public TVector Direction => _direction;
        public float Distance => _distance;
        public TRaycastHit[] HitCache => _hitCache;
        public TCollider[] ColliderCache => _colliderCache;
        public TAdvancedOptions Advanced => _advanced;
        public Vector3 LossyScale => _space.lossyScale;

        private readonly Matrix4x4 _space;
        private readonly TVector _origin;
        private readonly TVector _direction;
        private readonly float _distance;
        private readonly TRaycastHit[] _hitCache;
        private readonly TCollider[] _colliderCache;
        private readonly TAdvancedOptions _advanced;

        public PhysicsParameters(
            Matrix4x4 space,
            TVector origin,
            TVector direction,
            float distance,
            TRaycastHit[] hitCache,
            TCollider[] colliderCache,
            TAdvancedOptions advanced)
        {
            _space = space;
            _origin = origin;
            _direction = direction;
            _distance = distance;
            _hitCache = hitCache;
            _colliderCache = colliderCache;
            _advanced = advanced;
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