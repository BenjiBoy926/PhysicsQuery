using System;
using UnityEngine;

namespace PQuery
{
    internal class ResultSort_Distance : ResultSort<ResultSort_Distance.Wrapper>
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
                return Hit.distance.CompareTo(other.Hit.distance);
            }
        }

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