namespace PQuery
{
    public abstract class ResultSortMinimal : ResultSortGeneric<MinimalRaycastHit>
    {
        public static readonly ResultSortMinimal None = new ResultSortMinimal_None();
        public static readonly ResultSortMinimal Distance = new ResultSortMinimal_Distance();
    }
    public class ResultSortMinimal_None : ResultSortMinimal
    {
        protected override bool WillSort => false;
        protected override int Compare(MinimalRaycastHit a, MinimalRaycastHit b)
        {
            return 0;
        }
    }
    public class ResultSortMinimal_Distance : ResultSortMinimal
    {
        protected override int Compare(MinimalRaycastHit a, MinimalRaycastHit b)
        {
            return a.Distance.CompareTo(b.Distance);
        }
    }
}