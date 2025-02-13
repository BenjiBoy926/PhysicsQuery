using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, ResultSort2D, PhysicsShape2D, AdvancedOptions2D>
    {
        protected override ResultSort2D GetNoneSort()
        {
            return ResultSort2D.None;
        }
        protected override MinimalRaycastHit MinimizeRaycastHit(RaycastHit2D raycastHit)
        {
            return new(raycastHit);
        }
        protected override Vector2 Wrap(Vector3 vector)
        {
            return vector;
        }
        protected override Vector3 Unwrap(Vector2 vector)
        {
            return vector;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Shape.DrawGizmo(GetParameters(), transform.position);
        }
    }
}