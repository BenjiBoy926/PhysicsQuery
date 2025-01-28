using System;
using UnityEngine;

namespace PQuery
{
    internal class ResultSort_None : ResultSort<ResultSort_None.Wrapper>
    {
        public readonly struct Wrapper : IComparable<Wrapper>
        {
            public readonly RaycastHit Hit;

            public Wrapper(RaycastHit hit)
            {
                Hit = hit; 
            }
            public int CompareTo(Wrapper other)
            {
                return 0;
            }
        }

        protected override bool WillSort => false;
        protected override Wrapper Wrap(RaycastHit hit)
        {
            return new(hit);
        }
        protected override RaycastHit Unwrap(Wrapper wrapper)
        {
            return wrapper.Hit;
        }
    }
}