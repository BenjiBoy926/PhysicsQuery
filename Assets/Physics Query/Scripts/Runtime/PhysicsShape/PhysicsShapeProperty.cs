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

        [SerializeField]
        private PhysicsShapeType _type = PhysicsShapeType.Ray;
        [SerializeReference]
        private PhysicsShape _shape = new PhysicsShape_Ray();
    } 
}