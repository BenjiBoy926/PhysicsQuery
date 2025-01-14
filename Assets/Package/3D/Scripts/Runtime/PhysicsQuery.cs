using System;
using UnityEngine;

namespace PhysicsQuery
{
    public abstract class PhysicsQuery : MonoBehaviour
    {
        public const float MinDirectionSqrMagnitude = 0.0001f;

        [SerializeField] 
        private Space _space = Space.Self;
        [SerializeField] 
        private Vector3 _origin = Vector3.zero;
        [SerializeField] 
        private Vector3 _direction = Vector3.forward;
        [SerializeField] 
        private float _maxDistance = Mathf.Infinity;
        [SerializeField] 
        private LayerMask _layerMask = Physics.DefaultRaycastLayers;
        [SerializeField] 
        private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.UseGlobal;
        [SerializeField] 
        private int _cacheSize = 8;
        private RaycastHit[] _hitCache;
        private Collider[] _colliderCache;

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
        public int CacheSize
        {
            get => _cacheSize;
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException($"Max cached hits must be non-negative");
                }
                _cacheSize = value;
            }
        }
        public bool IsEmpty => GetType() == typeof(EmptyQuery);

        public abstract bool Cast(out RaycastHit hit);
        public abstract int CastNonAlloc(out RaycastHit[] hits);
        public abstract bool Check();
        public abstract int OverlapNonAlloc(out Collider[] overlaps);

        internal RaycastHit[] GetHitCache()
        {
            if (CacheNeedsRebuild(_hitCache))
            {
                RebuildCache(ref _hitCache);
            }
            return _hitCache;
        }
        internal Collider[] GetColliderCache()
        {
            if (CacheNeedsRebuild(_colliderCache))
            {
                RebuildCache(ref _colliderCache);
            }
            return _colliderCache;
        }
        private bool CacheNeedsRebuild<TElement>(TElement[] cache)
        {
            return cache == null || cache.Length != _cacheSize;
        }
        private void RebuildCache<TElement>(ref TElement[] cache)
        {
            cache = new TElement[_cacheSize];
        }

        public Ray GetWorldRay()
        {
            return new(GetWorldOrigin(), GetWorldDirection());
        }
        public Vector3 GetWorldOrigin()
        {
            return _space == Space.Self ? transform.TransformPoint(_origin) : _origin;
        }
        public Vector3 GetWorldDirection()
        {
            return _space == Space.Self ? transform.TransformDirection(_direction) : _direction;
        }
        

        private void OnValidate()
        {
            _maxDistance = Mathf.Max(0, _maxDistance);
            _cacheSize = Mathf.Max(0, _cacheSize);
        }
    }
}
