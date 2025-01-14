using System;
using UnityEngine;

namespace PhysicsQuery
{
    public abstract class PhysicsQuery : MonoBehaviour
    {
        public const float MinDirectionSqrMagnitude = 0.0001f;

        [SerializeField] private Space _space = Space.Self;
        [SerializeField] private Vector3 _origin = Vector3.zero;
        [SerializeField] private Vector3 _direction = Vector3.forward;
        [SerializeField] private float _maxDistance = Mathf.Infinity;
        [SerializeField] private LayerMask _layerMask = Physics.DefaultRaycastLayers;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.UseGlobal;
        [SerializeField] private int _maxCachedHits = 8;
        private RaycastHit[] _hitCache;

        public Space Space
        {
            get => _space;
            set => _space = value;
        }
        public Vector3 Origin
        {
            get => _origin;
            set => _origin = value;
        }
        public Vector3 Direction
        {
            get => _direction;
            set
            {
                if (_direction.sqrMagnitude < MinDirectionSqrMagnitude)
                {
                    throw new InvalidOperationException($"Direction vector cannot be zero (must have a squared magnitude greater than {MinDirectionSqrMagnitude})");
                }
                _direction = value;
            }
        }
        public float MaxDistance
        {
            get => _maxDistance;
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Max distance must be non-negative");
                }
                _maxDistance = value;
            }
        }
        public LayerMask LayerMask
        {
            get => _layerMask;
            set => _layerMask = value;
        }
        public QueryTriggerInteraction TriggerInteraction
        {
            get => _triggerInteraction;
            set => _triggerInteraction = value;
        }
        public int MaxCachedHits
        {
            get => _maxCachedHits;
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException($"Max cached hits must be non-negative");
                }
                _maxCachedHits = value;
            }
        }
        public bool IsEmpty => GetType() == typeof(EmptyQuery);

        public abstract bool Cast();
        public abstract bool Cast(out RaycastHit hit);
        public abstract RaycastHit[] CastAll();
        public abstract int CastNonAlloc(out RaycastHit[] hits);
        public abstract bool Check();
        public abstract RaycastHit[] Overlap();
        public abstract int OverlapNonAlloc(out RaycastHit[] hits);

        internal RaycastHit[] GetHitCache()
        {
            if (HitCacheNeedsRebuild())
            {
                RebuildHitCache();
            }
            return _hitCache;
        }
        internal Ray GetWorldRay()
        {
            if (_space == Space.Self)
            {
                Vector3 start = transform.TransformPoint(_origin);
                Vector3 end = transform.TransformPoint(_origin + _direction);
                return new(start, end - start);
            }
            return new(_origin, _direction);
        }
        private bool HitCacheNeedsRebuild()
        {
            return _hitCache == null || _hitCache.Length != _maxCachedHits;
        }
        private void RebuildHitCache()
        {
            _hitCache = new RaycastHit[_maxCachedHits];
        }

        private void OnValidate()
        {
            _maxDistance = Mathf.Max(0, _maxDistance);
            _maxCachedHits = Mathf.Max(0, _maxCachedHits);
        }
    }
}
