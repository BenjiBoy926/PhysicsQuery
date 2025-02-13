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

        [SerializeField]
        private Space _space;

        public Matrix4x4 GetTransformationMatrix()
        {
            return _space == Space.World ? Matrix4x4.identity : transform.localToWorldMatrix;
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

        public abstract bool MinimalCast(out MinimalRaycastHit hit);
        public abstract Result<MinimalRaycastHit> MinimalCastNonAlloc(ResultSortMinimal resultSort);
        public abstract bool Check();
        public abstract Result<Component> MinimalOverlapNonAlloc();
    }
}