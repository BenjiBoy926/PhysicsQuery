using System;
using System.Collections.Generic;
using UnityEngine;

namespace PQuery
{
    public class SubtypeDropdownAttribute : PropertyAttribute
    {
        public Type this[int i]
        {
            get => _subtypes[i];
        }
        public int Count => _subtypes.Count;

        private readonly List<Type> _subtypes;

        public SubtypeDropdownAttribute(params Type[] subtypes)
        {
            _subtypes = new(subtypes);
        }

        public int IndexOf(Type type)
        {
            return _subtypes.IndexOf(type);
        }
    }
}