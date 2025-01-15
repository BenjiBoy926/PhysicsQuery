using UnityEngine;

namespace PhysicsQuery
{
    public class BoxQuery : PhysicsQuery
    {
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

        public override PhysicsCastResult Cast()
        {
            Ray worldRay = GetWorldRay();
            RaycastHit[] hits = GetHitCache();
            int hitCount = Physics.BoxCastNonAlloc(worldRay.origin, Extents, worldRay.direction, hits, GetWorldOrientation(), MaxDistance, LayerMask);
            return new(hits, hitCount);
        }
        public override PhysicsOverlapResult Overlap()
        {
            Vector3 center = GetWorldOrigin();
            Collider[] overlaps = GetColliderCache();
            int overlapCount = Physics.OverlapBoxNonAlloc(center, Extents, overlaps, GetWorldOrientation(), LayerMask, TriggerInteraction);
            return new(overlaps, overlapCount);
        }

        public Quaternion GetWorldOrientation()
        {
            return Space == Space.Self ? transform.rotation * _orientation : _orientation;
        }
    }
}