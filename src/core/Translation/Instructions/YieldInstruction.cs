namespace Vezel.Niru.Translation.Instructions;

public sealed class YieldInstruction : Instruction
{
    public override bool IsTerminator => true;

    public override bool HasSideEffects => false;

    public Variable Address { get; }

    internal YieldInstruction(BasicBlock block, Variable address)
        : base(block)
    {
        Check.Null(address);
        Check.Argument((address.Unit, address.Type) == (block.Unit, TypeId.Int64), address);

        Address = address;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Address);
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
        return $"yield {Address}";
    }
}
