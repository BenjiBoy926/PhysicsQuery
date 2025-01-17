namespace PhysicsQuery
{
    internal class Cache<TElement>
    {
        private TElement[] _array;

        public TElement[] GetArray(int capacity)
        {
            if (NeedsRebuild(capacity))
            {
                Rebuild(capacity);
            }
            return _array;
        }
        private bool NeedsRebuild(int capacity)
        {
            return _array == null || _array.Length != capacity;
        }
        private void Rebuild(int capacity)
        {
            _array = new TElement[capacity];
        }
    }
}