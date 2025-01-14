using UnityEngine;

namespace PhysicsQuery
{
    public class BoxQuery : PhysicsQuery
    {
        public Vector3 Extents
        {
            get => _extents;
            set => _extents = value;
        }
        public Quaternion Orientation
        {
            get => _orientation;
            set => _orientation = value;
        }

        [Space]
        [SerializeField]
        private Vector3 _extents = Vector3.one * 0.5f;
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;

        public override int Cast(out RaycastHit[] hits)
        {
            Ray worldRay = GetWorldRay();
            hits = GetHitCache();
            return Physics.BoxCastNonAlloc(worldRay.origin, GetWorldExtents(), worldRay.direction, hits, GetWorldOrientation(), MaxDistance, LayerMask);
        }
        public override int Overlap(out Collider[] overlaps)
        {
            Vector3 center = GetWorldOrigin();
            overlaps = GetColliderCache();
            return Physics.OverlapBoxNonAlloc(center, GetWorldExtents(), overlaps, GetWorldOrientation(), LayerMask, TriggerInteraction);
        }

        public Vector3 GetWorldSize()
        {
            return GetWorldExtents() * 2;
        }
        public Vector3 GetWorldExtents()
        {
            if (Space == Space.World)
            {
                return _extents;
            }
            Vector3 lossyScale = transform.lossyScale;
            return new(_extents.x * lossyScale.x, _extents.y * lossyScale.y, _extents.z * lossyScale.z);
        }
        public Quaternion GetWorldOrientation()
        {
            return Space == Space.Self ? transform.rotation * _orientation : _orientation;
        }
    }
}