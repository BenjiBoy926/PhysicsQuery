using UnityEngine;

namespace PhysicsQuery
{
    public class EmptyQuery : PhysicsQuery
    {
        public override PhysicsCastResult Cast()
        {
            return PhysicsCastResult.Empty;
        }
        public override PhysicsOverlapResult Overlap()
        {
            return PhysicsOverlapResult.Empty;
        }
    }
}
