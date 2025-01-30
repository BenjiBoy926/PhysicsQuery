using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PQuery
{
    public abstract class PhysicsShape
    {
        private static readonly Dictionary<PhysicsShapeType, Type> _enumToType = new()
        {
            { PhysicsShapeType.Box, typeof(PhysicsShape_Box) },
            { PhysicsShapeType.Capsule, typeof(PhysicsShape_Capsule) },
            { PhysicsShapeType.Ray, typeof(PhysicsShape_Ray) },
            { PhysicsShapeType.Sphere, typeof(PhysicsShape_Sphere) }
        };
        private static readonly Type[] _noArgs = new Type[0];

        public static PhysicsShape Create(PhysicsShapeType type)
        {
            if (!_enumToType.ContainsKey(type))
            {
                throw new NotImplementedException($"Enum value {type} has no corresponding class definition");
            }
            Type definition = _enumToType[type];
            ConstructorInfo constructor = definition.GetConstructor(_noArgs);
            if (constructor == null)
            {
                throw new NotImplementedException($"Expected {definition.Name} to have a parameterless constructor defined, " +
                    $"but no such definition could be found");
            }
            object result = constructor.Invoke(null);
            if (result is not PhysicsShape shape)
            {
                throw new NotImplementedException($"{definition.Name} must be a subtype of {nameof(PhysicsShape)}");
            }
            return shape;
        }

        public abstract bool Cast(PhysicsQuery query, Ray worldRay, out RaycastHit hit);
        public abstract int CastNonAlloc(PhysicsQuery query, Ray worldRay, RaycastHit[] cache);
        public abstract bool Check(PhysicsQuery query, Vector3 worldOrigin);
        public abstract int OverlapNonAlloc(PhysicsQuery query, Vector3 worldOrigin, Collider[] cache);
    }
}