using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort3D : ResultSortGeneric<RaycastHit>
    {
        public static readonly ResultSort3D None = new ResultSort3D_None();
        public static readonly ResultSort3D Distance = new ResultSort3D_Distance();
    }
}