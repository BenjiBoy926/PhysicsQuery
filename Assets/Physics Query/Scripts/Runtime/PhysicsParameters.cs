using UnityEngine;

namespace PQuery
{
    public struct PhysicsParameters : IPhysicsParameters
    {
        public readonly Vector3 LossyScale => Space.lossyScale;

        public Matrix4x4 Space;
        public LayerMask LayerMask;
        public QueryTriggerInteraction TriggerInteraction;
        public Vector3 Start;
        public Vector3 End;
        public RaycastHit[] HitCache;
        public Collider[] ColliderCache;

        public void Snapshot(PhysicsQuery query)
        {
            PhysicsQuery3D query3D = (PhysicsQuery3D)query;
            Space = query3D.GetTransformationMatrix();
            LayerMask = query3D.LayerMask;
            TriggerInteraction = query3D.TriggerInteraction;
            Start = query3D.Start.Unwrap();
            End = query3D.End.Unwrap();
            HitCache = query3D.GetHitCache();
            ColliderCache = query3D.GetColliderCache();
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