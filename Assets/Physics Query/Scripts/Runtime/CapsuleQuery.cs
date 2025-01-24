using UnityEngine;

namespace PhysicsQuery
{
    public class CapsuleQuery : PhysicsQuery
    {
        public Vector3 Axis
        {
            get => _axis;
            set => _axis = value;
        }
        public float Height
        {
            get => _height;
            set => _height = Mathf.Max(value, MinNonZeroFloat);
        }
        public float Extent
        {
            get => _height / 2;
            set => _height = value * 2;
        }
        public float Radius
        {
            get => _radius;
            set => _radius = Mathf.Max(value, MinNonZeroFloat);
        }

        [Space]
        [SerializeField]
        private Vector3 _axis = Vector3.up;
        [SerializeField]
        private float _height = 1;
        [SerializeField]
        private float _radius = 0.5f;

        protected override int PerformCast(Ray worldRay, RaycastHit[] cache)
        {
            GetCapPositions(worldRay.origin, out Vector3 cap1, out Vector3 cap2);
            return Physics.CapsuleCastNonAlloc(cap1, cap2, _radius, worldRay.direction, cache, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int PerformOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            GetCapPositions(worldOrigin, out Vector3 cap1, out Vector3 cap2);
            return Physics.OverlapCapsuleNonAlloc(cap1, cap2, _radius, cache, LayerMask, TriggerInteraction);
        }

        public void GetCapPositions(Vector3 worldOrigin, out Vector3 cap1, out Vector3 cap2)
        {
            Vector3 worldAxis = GetWorldAxis();
            cap1 = worldOrigin + worldAxis;
            cap2 = worldOrigin - worldAxis;
        }
        public Vector3 GetWorldAxis()
        {
            if (Space == Space.World)
            {
                return _axis.normalized * Extent;
            }
            return transform.TransformDirection(_axis).normalized * Extent;
        }
    }
}