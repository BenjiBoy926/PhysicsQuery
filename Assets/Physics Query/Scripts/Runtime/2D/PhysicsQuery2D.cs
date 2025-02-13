using UnityEngine;

namespace PQuery
{
    public class PhysicsQuery2D : PhysicsQueryGeneric<Vector2, RaycastHit2D, Collider2D, ResultSort2D, PhysicsShape2D>
    {
        protected override ResultSort2D GetSort(ResultSortType sortType)
        {
            return sortType == ResultSortType.None ? ResultSort2D.None : ResultSort2D.Distance;
        }
        protected override MinimalRaycastHit Minimize(RaycastHit2D raycastHit)
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