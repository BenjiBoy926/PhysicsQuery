using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, PhysicsQuery2D.Shape, AdvancedOptions2D>
    {
        public abstract class Shape : AbstractShape
        {
            public Matrix4x4 GetGizmoTransformMatrix(Parameters parameters, Vector2 center, float localAngle)
            {
                float worldAngle = localAngle + GetTransformAngle(parameters);
                Quaternion rotation = Quaternion.Euler(0, 0, worldAngle);
                return Matrix4x4.TRS(center, rotation, parameters.LossyScale);
            }
            public float GetTransformAngle(Parameters parameters)
            {
                Vector3 worldUp = parameters.Space.GetColumn(1);
                Vector3 projectedUp = new(worldUp.x, worldUp.y, 0);
                return Vector3.SignedAngle(projectedUp, Vector3.up, Vector3.back);
            }
        }
    }
}