namespace PhysicsQuery
{
    internal class CachedArray<TElement>
    {
        private TElement[] _array;

        public TElement[] GetArray(int capacity)
        {
            SetCapacity(capacity);
            return _array;
        }
        public void SetCapacity(int capacity)
        {
            if (IsAllocationNeeded(capacity))
            {
                Allocate(capacity);
            }
        }
        private bool IsAllocationNeeded(int capacity)
        {
            return _array == null || _array.Length != capacity;
        }
        private void Allocate(int capacity)
        {
            _array = new TElement[capacity];
        }
    }
}