using System;
using UnityEngine;

namespace PQuery
{
    // Put enum and class together so that it plays well with multi object editing in the inspector
    [Serializable]
    public class PhysicsShapeProperty
    {
        public const string TypeFieldName = nameof(_type);
        public const string ShapeFieldName = nameof(_shape);

        internal PhysicsShape Shape => _shape;

        [SerializeField]
        private PhysicsShapeType _type = PhysicsShapeType.Ray;
        [SerializeReference]
        private PhysicsShape _shape = new PhysicsShape_Ray();

        public bool Cast(PhysicsQuery query, Ray worldRay, out RaycastHit hit)
        {
            return _shape.Cast(query, worldRay, out hit);
        }
        public int CastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache)
        {
            return _shape.CastNonAlloc(query, worldRay, cache);
        }
        public bool Check(PhysicsQuery query, Vector3 worldOrigin)
        {
            return _shape.Check(query, worldOrigin);
        }
        public int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache)
        {
            return _shape.OverlapNonAlloc(query, worldOrigin, cache);
        }
    }
}