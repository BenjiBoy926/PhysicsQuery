using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, ResultSort2D, PhysicsShape2D>
    {
        public override float Magnitude(Vector2 vector)
        {
            return vector.magnitude;
        }
        public override Vector2 Normalize(Vector2 vector)
        {
            return vector.normalized;
        }
        public override Vector2 Subtract(Vector2 minuend, Vector2 subtrahend)
        {
            return minuend - subtrahend;
        }
        public override Vector2 TransformAsPoint(Vector2 point)
        {
            return transform.TransformPoint(point);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Shape.DrawGizmo(GetParameters(), transform.position);
        }
    }
}