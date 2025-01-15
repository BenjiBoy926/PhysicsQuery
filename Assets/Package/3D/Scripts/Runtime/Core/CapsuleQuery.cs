using UnityEngine;

namespace PhysicsQuery
{
    public class CapsuleQuery : PhysicsQuery
    {
        public Vector3 OtherCapPosition
        {
            get => _otherCapPosition;
            set => _otherCapPosition = value;
        }
        public float Radius
        {
            get => _radius;
            set => _radius = Mathf.Max(value, MinNonZeroFloat);
        }

        [Space]
        [SerializeField]
        private Vector3 _otherCapPosition = Vector3.down;
        [SerializeField]
        private float _radius = 0.5f;

        protected override int PerformCast(Ray worldRay, RaycastHit[] cache)
        {
            Vector3 otherCapWorldPosition = GetOtherCapWorldPosition();
            return Physics.CapsuleCastNonAlloc(worldRay.origin, otherCapWorldPosition, _radius, worldRay.direction, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int PerformOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            Vector3 otherCapWorldPosition = GetOtherCapWorldPosition();
            return Physics.OverlapCapsuleNonAlloc(worldOrigin, otherCapWorldPosition, _radius, cache, LayerMask, TriggerInteraction);
        }

        public Vector3 GetOtherCapWorldPosition()
        {
            if (Space == Space.World)
            {
                return _otherCapPosition;
            }
            return transform.TransformPoint(_otherCapPosition);
        }
    }
}