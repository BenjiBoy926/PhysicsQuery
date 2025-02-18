namespace PQuery
{
    public static class ResultSortMinimal
    {
        public static readonly ResultSort<MinimalRaycastHit> None = new ResultSortMinimal_None();
        public static readonly ResultSort<MinimalRaycastHit> Distance = new ResultSortMinimal_Distance();
    }
    public class ResultSortMinimal_None : ResultSort_None<MinimalRaycastHit>
    {

    }
    public class ResultSortMinimal_Distance : ResultSort_Distance<MinimalRaycastHit>
    {
        protected override float Distance(MinimalRaycastHit hit)
        {
            return hit.Distance;
        }
    }
}