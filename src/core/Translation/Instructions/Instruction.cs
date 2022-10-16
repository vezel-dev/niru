namespace Vezel.Niru.Translation.Instructions;

public abstract class Instruction : IIntrusiveLinkedListNode<Instruction>
{
    private protected static IReadOnlySet<Variable> EmptyVariables { get; } = new HashSet<Variable>();

    private protected static IReadOnlySet<BasicBlock> EmptyBlocks { get; } = new HashSet<BasicBlock>();

    public BasicBlock Block { get; }

    internal IntrusiveLinkedList<Instruction>? List { get; private set; }

    IntrusiveLinkedList<Instruction>? IIntrusiveLinkedListNode<Instruction>.List
    {
        get => List;
        set => List = value;
    }

    public Instruction? Previous { get; private set; }

    Instruction? IIntrusiveLinkedListNode<Instruction>.Previous
    {
        get => Previous;
        set => Previous = value;
    }

    public Instruction? Next { get; private set; }

    Instruction? IIntrusiveLinkedListNode<Instruction>.Next
    {
        get => Next;
        set => Next = value;
    }

    public abstract bool IsTerminator { get; }

    public abstract bool HasSideEffects { get; }

    public IReadOnlySet<Variable> Reads => _reads ??= GetReads();

    public IReadOnlySet<Variable> Writes => _writes ??= GetWrites();

    public IReadOnlySet<BasicBlock> Targets => _targets ??= GetTargets();

    private IReadOnlySet<Variable>? _reads;

    private IReadOnlySet<Variable>? _writes;

    private IReadOnlySet<BasicBlock>? _targets;

    private protected Instruction(BasicBlock block)
    {
        Check.Null(block);

        Block = block;
    }

    internal void Attach()
    {
        foreach (var read in Reads)
            read.AddRead(this);

        foreach (var write in Writes)
            write.AddWrite(this);

        foreach (var target in Targets)
        {
            Block.AddSuccessor(target);
            target.AddPredecessor(Block);
        }
    }

    internal void Detach()
    {
        foreach (var target in Targets)
        {
            target.RemovePredecessor(Block);
            Block.RemoveSuccessor(target);
        }

        foreach (var write in Writes)
            write.RemoveWrite(this);

        foreach (var read in Reads)
            read.RemoveRead(this);
    }

    public void Kill()
    {
        Check.Operation(List != null);

        Block.Remove(this);
    }

    private protected static IReadOnlySet<T> CreateSet<T>(T item1)
    {
        return new HashSet<T>(1)
        {
            item1,
        };
    }

    private protected static IReadOnlySet<T> CreateSet<T>(T item1, T item2)
    {
        return new HashSet<T>(2)
        {
            item1,
            item2,
        };
    }

    private protected static IReadOnlySet<T> CreateSet<T>(T item1, T item2, T item3)
    {
        return new HashSet<T>(3)
        {
            item1,
            item2,
            item3,
        };
    }

    private protected abstract IReadOnlySet<Variable> GetReads();

    private protected abstract IReadOnlySet<Variable> GetWrites();

    private protected abstract IReadOnlySet<BasicBlock> GetTargets();

    public abstract override string ToString();
}
