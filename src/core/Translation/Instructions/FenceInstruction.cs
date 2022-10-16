namespace Vezel.Niru.Translation.Instructions;

public sealed class FenceInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => true;

    public MemoryOrder Order { get; }

    internal FenceInstruction(BasicBlock block, MemoryOrder order)
        : base(block)
    {
        Check.Enum(order);

        Order = order;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return EmptyVariables;
    }

    private protected override IReadOnlySet<Variable> GetWrites()
    {
        return EmptyVariables;
    }

    private protected override IReadOnlySet<BasicBlock> GetTargets()
    {
        return EmptyBlocks;
    }

    public override string ToString()
    {
        return $"fence {Order.ToIRString()}";
    }
}
