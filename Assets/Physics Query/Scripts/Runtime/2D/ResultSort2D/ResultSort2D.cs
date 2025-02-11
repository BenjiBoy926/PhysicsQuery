using UnityEngine;

namespace PQuery
{
    public abstract class ResultSort2D : ResultSortGeneric<RaycastHit2D>
    {
        public static readonly ResultSort2D None = new ResultSort2D_None();
        public static readonly ResultSort2D Distance = new ResultSort2D_Distance();
    }
}