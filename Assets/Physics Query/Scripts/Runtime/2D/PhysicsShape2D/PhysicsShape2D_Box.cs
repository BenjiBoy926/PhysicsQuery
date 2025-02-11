using System;
using UnityEngine;

namespace PQuery
{
    public class PhysicsShape2D_Box : PhysicsShape2D
    {
        [SerializeField]
        private Vector2 _size = Vector2.one;
        [SerializeField]
        private float _angle = 0;

        public override bool Cast(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, out RaycastHit2D hit)
        {
            throw new NotImplementedException();
        }
        public override Result<RaycastHit2D> CastNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            throw new NotImplementedException();
        }
        public override bool Check(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            throw new NotImplementedException();
        }
        public override Result<Collider2D> OverlapNonAlloc(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            throw new NotImplementedException();
        }

        public override void DrawOverlapGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            DrawGizmo(parameters, parameters.GetWorldStart());
        }
        public override void DrawGizmo(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters, VectorWrapper2D center)
        {

        }

        public Vector2 GetWorldSize(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return _size * parameters.LossyScale;
        }
        public float GetWorldAngle(PhysicsParameters<VectorWrapper2D, RayWrapper2D, RaycastHit2D, Collider2D> parameters)
        {
            return _angle + parameters.Space.rotation.eulerAngles.z;
        }
    }
}