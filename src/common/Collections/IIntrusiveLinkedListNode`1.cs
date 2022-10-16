namespace Vezel.Niru.Collections;

internal interface IIntrusiveLinkedListNode<T>
    where T : class, IIntrusiveLinkedListNode<T>
{
    public IntrusiveLinkedList<T>? List { get; set; }

    public T? Previous { get; set; }

    public T? Next { get; set; }
}
