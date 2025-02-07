namespace PQuery
{
    public interface IResultSort<TRaycastHit>
    {
        void Sort(TRaycastHit[] cache, int count);
    }
}