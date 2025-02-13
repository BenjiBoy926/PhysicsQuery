using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShape2D : PhysicsShapeGeneric<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D>
    {
        public Matrix4x4 GetGizmoTransformMatrix(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters, Vector2 center, float localAngle)
        {
            float worldAngle = localAngle + GetTransformAngle(parameters);
            Quaternion rotation = Quaternion.Euler(0, 0, worldAngle);
            return Matrix4x4.TRS(center, rotation, parameters.LossyScale);
        }
        public float GetTransformAngle(PhysicsParameters<Vector2, RaycastHit2D, Collider2D, AdvancedOptions2D> parameters)
        {
            Vector3 worldUp = parameters.Space.GetColumn(1);
            Vector3 projectedUp = new(worldUp.x, worldUp.y, 0);
            return Vector3.SignedAngle(projectedUp, Vector3.up, Vector3.back);
        }
    }
}