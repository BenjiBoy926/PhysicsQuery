using UnityEngine;

namespace PQuery
{
    public partial class PhysicsQuery3D : PhysicsQueryGeneric<Vector3, RaycastHit, Collider, ResultSort3D, PhysicsQuery3D.Shape, AdvancedOptions3D>
    {
        public abstract class Shape : AbstractShape
        {

        }
    }
}