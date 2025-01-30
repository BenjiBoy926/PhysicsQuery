using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PQuery
{
    // Put enum and class together so that it plays well with multi object editing in the inspector
    [Serializable]
    public class PhysicsShapePair
    {
        public const string TypeFieldName = nameof(_type);
        public const string ShapeFieldName = nameof(_shape);

        private static readonly List<PhysicsShapePair> _template = new()
        {
            new(PhysicsShapeType.Box, new PhysicsShape_Box()),
            new(PhysicsShapeType.Capsule, new PhysicsShape_Capsule()),
            new(PhysicsShapeType.Ray, new PhysicsShape_Ray()),
            new(PhysicsShapeType.Sphere, new PhysicsShape_Sphere())
        };
        private static readonly Type[] _noArgs = new Type[0];
        internal PhysicsShape Shape => _shape;

        [SerializeField]
        private PhysicsShapeType _type = PhysicsShapeType.Ray;
        [SerializeReference]
        private PhysicsShape _shape = new PhysicsShape_Ray();

        internal PhysicsShapePair(PhysicsShapeType type, PhysicsShape shape)
        {
            _type = type;
            _shape = shape;
        }

        public static PhysicsShape CreateShape(PhysicsShapeType type)
        {
            bool HasSameType(PhysicsShapePair pair) => pair._type == type;
            PhysicsShapePair templateItem = _template.Find(HasSameType) ??
                 throw new NotImplementedException($"Enum value {type} has no corresponding class definition");
            
            Type definition = templateItem._shape.GetType();
            ConstructorInfo constructor = definition.GetConstructor(_noArgs) ??
                throw new NotImplementedException($"Expected {definition.Name} to have a parameterless constructor defined, but no such definition could be found");
            
            return (PhysicsShape)constructor.Invoke(null);
        }

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