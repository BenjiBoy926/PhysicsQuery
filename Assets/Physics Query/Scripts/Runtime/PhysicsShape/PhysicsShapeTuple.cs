using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PQuery
{
    // Put enum and class together so that it plays well with multi object editing in the inspector
    [Serializable]
    public class PhysicsShapeTuple
    {
        public const string TypeFieldName = nameof(_type);
        public const string ShapeFieldName = nameof(_shape);

        private static readonly List<PhysicsShapeTuple> _template = new()
        {
            new(PhysicsShapeType.Box, new PhysicsShape_Box()),
            new(PhysicsShapeType.Capsule, new PhysicsShape_Capsule()),
            new(PhysicsShapeType.Ray, new PhysicsShape_Ray()),
            new(PhysicsShapeType.Sphere, new PhysicsShape_Sphere())
        };
        private static readonly Type[] _noArgs = new Type[0];
        public PhysicsShape Shape => _shape;
        private Type ShapeClassType => _shape.GetType();

        [SerializeField]
        private PhysicsShapeType _type = PhysicsShapeType.Ray;
        [SerializeReference]
        private PhysicsShape _shape = new PhysicsShape_Ray();

        internal PhysicsShapeTuple() : this(PhysicsShapeType.Ray, new PhysicsShape_Ray()) { }
        internal PhysicsShapeTuple(PhysicsShapeType type, PhysicsShape shape)
        {
            _type = type;
            _shape = shape;
        }

        public static PhysicsShape CreateShape(PhysicsShapeType type)
        {
            PhysicsShapeTuple templateItem = _template.Find(x => x._type == type) ??
                 throw new NotImplementedException(MissingTemplateMessage(type.ToString()));
            
            Type definition = templateItem.ShapeClassType;
            ConstructorInfo constructor = definition.GetConstructor(_noArgs) ??
                throw new NotImplementedException($"Expected {definition.Name} to have a parameterless constructor defined, but no such definition could be found");
            
            return (PhysicsShape)constructor.Invoke(null);
        }
        public void SetShape(PhysicsShape shape)
        {
            Type intendedShapeType = shape.GetType();
            PhysicsShapeTuple templateItem = _template.Find(x => x.ShapeClassType == intendedShapeType) ??
                throw new NotImplementedException(MissingTemplateMessage(intendedShapeType.Name));

            _type = templateItem._type;
            _shape = shape;
        }
        private static string MissingTemplateMessage(string itemName)
        {
            return $"{itemName} does not exist in the {nameof(PhysicsShapeTuple)} template";
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

        public void DrawOverlapGizmo(PhysicsQuery query)
        {
            _shape.DrawOverlapGizmo(query);
        }
        public void DrawGizmo(PhysicsQuery query, Vector3 center)
        {
            _shape.DrawGizmo(query, center);
        }
    }
}