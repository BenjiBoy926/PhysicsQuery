using UnityEngine;

namespace PhysicsQuery
{
    public class BoxQuery : PhysicsQuery
    {
        protected override PreviewShape Shape => new PreviewShape_Box(this);
        public Vector3 Size
        {
            get => _size;
            set => _size = value;
        }
        public Vector3 Extents
        {
            get => _size / 2;
            set => _size = value * 2;
        }
        public Quaternion Orientation
        {
            get => _orientation;
            set => _orientation = value;
        }

        [Space]
        [SerializeField]
        private Vector3 _size = Vector3.one;
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;

        protected override int PerformCast(Ray worldRay, RaycastHit[] cache)
        {
            Quaternion worldOrientation = GetWorldOrientation();
            return Physics.BoxCastNonAlloc(worldRay.origin, Extents, worldRay.direction, cache, worldOrientation, MaxDistance, LayerMask, TriggerInteraction);
        }
        protected override int PerformOverlap(Vector3 worldOrigin, Collider[] cache)
        {
            Quaternion worldOrientation = GetWorldOrientation();
            return Physics.OverlapBoxNonAlloc(worldOrigin, Extents, cache, worldOrientation, LayerMask, TriggerInteraction);
        }

        public Quaternion GetWorldOrientation()
        {
            return Space == Space.Self ? transform.rotation * _orientation : _orientation;
        }
    }
}