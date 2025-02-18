using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, PhysicsQuery2D.Shape, AdvancedOptions2D>
    {
        protected override Shape GetDefaultShape()
        {
            return new RayShape();
        }
        protected override AdvancedOptions2D GetDefaultOptions()
        {
            return AdvancedOptions2D.Default;
        }
        protected override ResultSort<RaycastHit2D> GetNoneSort()
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
            CurrentShape.DrawGizmo(GetParameters(), transform.position);
        }
    }
}