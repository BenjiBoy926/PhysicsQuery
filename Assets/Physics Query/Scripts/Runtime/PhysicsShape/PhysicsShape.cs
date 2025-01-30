using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShape
    {
        public abstract bool Cast(PhysicsQuery query, Ray worldRay, out RaycastHit hit);
        public abstract int CastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache);
        public abstract bool Check(PhysicsQuery query, Vector3 worldOrigin);
        public abstract int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache);
    }
}