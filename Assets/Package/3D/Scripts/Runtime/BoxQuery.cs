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
        private Vector3 _extents = Vector3.one;
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;

        public override bool Cast(out RaycastHit hit)
        {
            Ray worldRay = GetWorldRay();
            return Physics.BoxCast(worldRay.origin, GetWorldExtents(), worldRay.direction, out hit, GetWorldOrientation(), MaxDistance, LayerMask, TriggerInteraction);
        }
        public override int CastNonAlloc(out RaycastHit[] hits)
        {
            Ray worldRay = GetWorldRay();
            hits = GetHitCache();
            return Physics.BoxCastNonAlloc(worldRay.origin, GetWorldExtents(), worldRay.direction, hits, GetWorldOrientation(), MaxDistance, LayerMask);
        }
        public override bool Check()
        {
            Vector3 center = GetWorldOrigin();
            return Physics.CheckBox(center, GetWorldExtents(), GetWorldOrientation(), LayerMask, TriggerInteraction);
        }
        public override int OverlapNonAlloc(out Collider[] overlaps)
        {
            Vector3 center = GetWorldOrigin();
            overlaps = GetColliderCache();
            return Physics.OverlapBoxNonAlloc(center, GetWorldExtents(), overlaps, GetWorldOrientation(), LayerMask, TriggerInteraction);
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