#region

using System.Collections.Generic;

#endregion

namespace cope
{
    /// <summary>
    /// Implements a heap with a custom IComparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomHeap<T> : DelegateHeap<T>
    {
        public CustomHeap(IEnumerable<T> t, IComparer<T> comp) : base(t, comp.Compare)
        {
        }

        public CustomHeap(DelegateHeap<T> heap, IComparer<T> comp)
            : base(heap, comp.Compare)
        {
        }

        public CustomHeap(CustomHeap<T> heap) : base(heap)
        {
        }
    }
}