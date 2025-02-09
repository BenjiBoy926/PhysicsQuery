using System.Collections;
using UnityEngine;

namespace PQuery
{
    public interface IRay<TVector> where TVector : IVector<TVector>
    {
        TVector Origin { get; set; }
        TVector Direction { get; set; }
        TVector GetPoint(float distance);
    }
}