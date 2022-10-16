using Vezel.Niru.Translation.Instructions;

namespace Vezel.Niru.Translation;

public sealed class BasicBlock
{
    public TranslationUnit Unit { get; }

    public int Id { get; }

    public bool IsEntry => this == Unit.Entry;

    public IReadOnlyCollection<Instruction> Instructions => _instructions;

    public IReadOnlySet<BasicBlock> Predecessors => _predecessors;

    public IReadOnlySet<BasicBlock> Successors => _successors;

    public Instruction? Terminator { get; private set; }

    private static readonly Comparer<BasicBlock> _blockComparer =
        Comparer<BasicBlock>.Create((x, y) => x.Id.CompareTo(y.Id));

    private readonly SortedSet<BasicBlock> _predecessors = new(_blockComparer);

    private readonly SortedSet<BasicBlock> _successors = new(_blockComparer);

    private readonly IntrusiveLinkedList<Instruction> _instructions = new();

    internal BasicBlock(TranslationUnit unit, int id)
    {
        Unit = unit;
        Id = id;
    }

    private void Add(Instruction instruction)
    {
        instruction.Attach();

        if (instruction.IsTerminator)
            Terminator = instruction;
    }

    private void AddFirst(Instruction instruction)
    {
        _instructions.AddFirst(instruction);

        Add(instruction);
    }

    private void AddLast(Instruction instruction)
    {
        _instructions.AddLast(instruction);

        Add(instruction);
    }

    private void AddBefore(Instruction before, Instruction instruction)
    {
        _instructions.AddBefore(before, instruction);

        Add(instruction);
    }

    private void AddAfter(Instruction after, Instruction instruction)
    {
        _instructions.AddAfter(after, instruction);

        Add(instruction);
    }

    public CodeEmitter EmitFirst()
    {
        return new(this, list =>
        {
            var current = list[0];

            AddFirst(current);

            foreach (var insn in list.Skip(1))
            {
                AddAfter(current, insn);

                current = insn;
            }
        });
    }

    public CodeEmitter EmitLast()
    {
        return new(this, list =>
        {
            Check.Operation(Terminator == null);

            var current = list[0];

            AddLast(current);

            foreach (var insn in list.Skip(1))
            {
                AddAfter(current, insn);

                current = insn;
            }
        });
    }

    public CodeEmitter EmitBefore(Instruction instruction)
    {
        Check.Null(instruction);
        Check.Argument(instruction.Block == this, instruction);

        return new(this, list =>
        {
            Check.Operation(instruction.List != null);

            var current = list[0];

            AddBefore(instruction, current);

            foreach (var insn in list.Skip(1))
            {
                AddAfter(current, insn);

                current = insn;
            }
        });
    }

    public CodeEmitter EmitAfter(Instruction instruction)
    {
        Check.Null(instruction);
        Check.Argument(instruction.Block == this && !instruction.IsTerminator, instruction);

        return new(this, list =>
        {
            Check.Operation(instruction.List != null);

            var current = instruction;

            foreach (var insn in list)
            {
                AddAfter(current, insn);

                current = insn;
            }
        });
    }

    public void EmitFirst(Action<CodeEmitter> action)
    {
        Check.Null(action);

        using var code = EmitFirst();

        action(code);
    }

    public void EmitLast(Action<CodeEmitter> action)
    {
        Check.Null(action);

        using var code = EmitLast();

        action(code);
    }

    public void EmitBefore(Instruction instruction, Action<CodeEmitter> action)
    {
        Check.Null(action);

        using var code = EmitBefore(instruction);

        action(code);
    }

    public void EmitAfter(Instruction instruction, Action<CodeEmitter> action)
    {
        Check.Null(action);

        using var code = EmitAfter(instruction);

        action(code);
    }

    internal void Remove(Instruction instruction)
    {
        if (instruction.IsTerminator)
            Terminator = null;

        instruction.Detach();

        _ = _instructions.Remove(instruction);
    }

    public void Clear()
    {
        var node = _instructions.First;

        while (node != null)
        {
            var next = node.Next;

            Remove(node);

            node = next;
        }
    }

    internal void AddPredecessor(BasicBlock block)
    {
        _ = _predecessors.Add(block);
    }

    internal void RemovePredecessor(BasicBlock block)
    {
        _ = _predecessors.Remove(block);
    }

    internal void AddSuccessor(BasicBlock block)
    {
        _ = _successors.Add(block);
    }

    internal void RemoveSuccessor(BasicBlock block)
    {
        _ = _successors.Remove(block);
    }

    public void Kill()
    {
        Check.Operation((IsEntry, _predecessors.Count, _successors.Count) == (false, 0, 0));

        Unit.RemoveBlock(this);
    }

    public override string ToString()
    {
        return $"%{Id}";
    }
}
