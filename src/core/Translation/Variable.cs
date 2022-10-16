using Vezel.Niru.Translation.Instructions;

namespace Vezel.Niru.Translation;

public abstract class Variable
{
    public TranslationUnit Unit { get; }

    public TypeId Type { get; }

    public IReadOnlySet<Instruction> Reads => _reads;

    public IReadOnlySet<Instruction> Writes => _writes;

    private readonly HashSet<Instruction> _reads = new();

    private readonly HashSet<Instruction> _writes = new();

    private protected Variable(TranslationUnit unit, TypeId type)
    {
        Unit = unit;
        Type = type;
    }

    internal void AddRead(Instruction instruction)
    {
        _ = _reads.Add(instruction);
    }

    internal void RemoveRead(Instruction instruction)
    {
        _ = _reads.Remove(instruction);
    }

    internal void AddWrite(Instruction instruction)
    {
        _ = _writes.Add(instruction);
    }

    internal void RemoveWrite(Instruction instruction)
    {
        _ = _writes.Remove(instruction);
    }

    public abstract override string ToString();
}
