namespace Vezel.Niru.Collections;

internal sealed class IntrusiveLinkedList<T> : IReadOnlyCollection<T>
    where T : class, IIntrusiveLinkedListNode<T>
{
    public struct Enumerator : IEnumerator<T>
    {
        public T Current => _current!;

        object? IEnumerator.Current => Current;

        private T? _current;

        private bool _started;

        internal Enumerator(T? first)
        {
            _current = first;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_started)
                _current = _current?.Next;

            _started = true;

            return _current != null;
        }

        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }
    }

    public T? First { get; private set; }

    public T? Last { get; private set; }

    public int Count { get; private set; }

    public IntrusiveLinkedList()
    {
    }

    public void AddFirst(T item)
    {
        if (First == null)
        {
            item.List = this;

            First = item;
            Last = item;
            Count = 1;
        }
        else
            AddBefore(First, item);
    }

    public void AddLast(T item)
    {
        if (Last == null)
        {
            item.List = this;

            First = item;
            Last = item;
            Count = 1;
        }
        else
            AddAfter(Last, item);
    }

    public void AddBefore(T node, T item)
    {
        var prev = node.Previous;

        item.Previous = prev;
        item.Next = node;
        node.Previous = item;

        if (prev != null)
            prev.Next = item;

        if (node == First)
            First = item;

        Count++;
    }

    public void AddAfter(T node, T item)
    {
        var next = node.Next;

        item.Previous = node;
        item.Next = next;
        node.Next = item;

        if (next != null)
            next.Previous = item;

        if (node == Last)
            Last = item;

        Count++;
    }

    public bool Remove(T item)
    {
        if (item.List != this)
            return false;

        var prev = item.Previous;
        var next = item.Next;

        if (prev != null)
            prev.Next = next;
        else
            First = next;

        if (next != null)
            next.Previous = prev;
        else
            Last = prev;

        item.Next = null;
        item.Previous = null;
        item.List = null;

        Count--;

        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(First);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
