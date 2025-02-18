using System;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsQuery : MonoBehaviour
    {
        public static event Action<PhysicsQuery> DrawGizmos = delegate { };
        public static event Action<PhysicsQuery> DrawGizmosSelected = delegate { };

        public Space Space
        {
            get => _space;
            set => _space = value;
        }
        protected CachedArray<AgnosticRaycastHit> AgnosticHitCache => _agnosticHitCache;
        protected CachedArray<BoxedRaycastHit> BoxedHitCache => _boxedHitCache;
        protected CachedArray<Component> AgnosticColliderCache => _agnosticColliderCache;

        [SerializeField]
        private Space _space;

        private readonly CachedArray<AgnosticRaycastHit> _agnosticHitCache = new();
        private readonly CachedArray<BoxedRaycastHit> _boxedHitCache = new();
        private readonly CachedArray<Component> _agnosticColliderCache = new();

        public Ray GetAgnosticWorldRay()
        {
            Vector3 start = GetAgnosticWorldStart();
            Vector3 end = GetAgnosticWorldEnd();
            return new Ray(start, end - start);
        }
        public Matrix4x4 GetTransformationMatrix()
        {
            return _space == Space.World ? Matrix4x4.identity : transform.localToWorldMatrix;
        }
        public virtual void RefreshCache()
        {
            RefreshAgnosticHitCache();
            RefreshBoxedHitCache();
            RefreshAgnosticColliderCache();
        }
        public void RefreshAgnosticHitCache()
        {
            _agnosticHitCache.SetCapacity(CacheCapacity);
        }
        public void RefreshBoxedHitCache()
        {
            _boxedHitCache.SetCapacity(CacheCapacity);
        }
        public void RefreshAgnosticColliderCache()
        {
            _agnosticColliderCache.SetCapacity(CacheCapacity);
        }

        protected virtual void Reset()
        {
            _space = Settings.DefaultQuerySpace;
        }
        protected virtual void OnDrawGizmos()
        {
            DrawGizmos(this);
        }
        protected virtual void OnDrawGizmosSelected()
        {
            DrawGizmosSelected(this);
        }

        public abstract int CacheCapacity { get; }
        public abstract Vector3 GetAgnosticWorldStart();
        public abstract Vector3 GetAgnosticWorldEnd();
        public abstract bool AgnosticCast(out AgnosticRaycastHit hit);
        public abstract bool BoxedCast(out BoxedRaycastHit hit);
        public abstract Result<AgnosticRaycastHit> AgnosticCastNonAlloc(ResultSort<AgnosticRaycastHit> resultSort);
        public abstract Result<BoxedRaycastHit> BoxedCastNonAlloc(ResultSort<BoxedRaycastHit> resultSort);
        public abstract bool Check();
        public abstract Result<Component> AgnosticOverlapNonAlloc();
        public abstract void DrawOverlapGizmo();
        public abstract void DrawGizmo(Vector3 center);
    }
}