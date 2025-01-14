using UnityEngine;

namespace PhysicsQuery
{
    public class BoxQuery : PhysicsQuery
    {
        [Space]
        [SerializeField]
        private Vector3 _extents;
        [SerializeField]
        private Quaternion _orientation;

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

        private Vector3 GetWorldExtents()
        {
            if (Space == Space.World)
            {
                return _extents;
            }
            Vector3 lossyScale = transform.lossyScale;
            return new(_extents.x * lossyScale.x, _extents.y * lossyScale.y, _extents.z * lossyScale.z);
        }
        private Quaternion GetWorldOrientation()
        {
            if (Space == Space.World)
            {
                return _orientation;
            }
            return transform.rotation * _orientation;
        }
    }
}