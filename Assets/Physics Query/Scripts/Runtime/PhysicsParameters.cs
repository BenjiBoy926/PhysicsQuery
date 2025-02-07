using UnityEngine;

namespace PQuery
{
    public readonly struct PhysicsParameters
    {
        public readonly Vector3 LossyScale => Space.lossyScale;

        public readonly Matrix4x4 Space;
        public readonly Vector3 Start;
        public readonly Vector3 End;
        public readonly LayerMask LayerMask;
        public readonly QueryTriggerInteraction TriggerInteraction;
        public readonly RaycastHit[] HitCache;
        public readonly Collider[] ColliderCache;

        public static PhysicsParameters Snapshot(PhysicsQuery3D query)
        {
            return new(
                query.GetTransformationMatrix(),
                query.Start,
                query.End,
                query.LayerMask,
                query.TriggerInteraction,
                query.GetHitCache(),
                query.GetColliderCache());
        }
        public PhysicsParameters(
            Matrix4x4 space,
            Vector3 start,
            Vector3 end,
            LayerMask layerMask,
            QueryTriggerInteraction triggerInteraction,
            RaycastHit[] hitCache,
            Collider[] colliderCache)
        {
            Space = space;
            Start = start;
            End = end;
            LayerMask = layerMask;
            TriggerInteraction = triggerInteraction;
            HitCache = hitCache;
            ColliderCache = colliderCache;
        }

        public RayDistance GetWorldRay()
        {
            return new(GetWorldStart(), GetWorldEnd());
        }
        public Vector3 GetWorldStart()
        {
            return TransformPoint(Start);
        }
        public Vector3 GetWorldEnd()
        {
            return TransformPoint(End);
        }
        public Vector3 TransformPoint(Vector3 point)
        {
            return Space.MultiplyPoint3x4(point);
        }
        public Vector3 TransformDirection(Vector3 direction)
        {
            return Space.MultiplyVector(direction);
        }
        public Quaternion TransformRotation(Quaternion rotation)
        {
            return Space.rotation * rotation;
        }
        public Vector3 TransformScale(Vector3 scale)
        {
            Vector3 lossyScale = LossyScale;
            return new(scale.x * lossyScale.x, scale.y * lossyScale.y, scale.z * lossyScale.z);
        }
    }
}