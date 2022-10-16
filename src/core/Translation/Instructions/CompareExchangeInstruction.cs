namespace Vezel.Niru.Translation.Instructions;

public sealed class CompareExchangeInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => true;

    public MemoryOrder Order { get; }

    public Variable Result { get; }

    public Variable Address { get; }

    public Variable Comparand { get; }

    public Variable Value { get; }

    internal CompareExchangeInstruction(
        BasicBlock block, MemoryOrder order, Variable result, Variable address, Variable comparand, Variable value)
        : base(block)
    {
        Check.Enum(order);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(address);
        Check.Argument((address.Unit, address.Type) == (block.Unit, TypeId.Int64), address);
        Check.Null(comparand);
        Check.Argument((comparand.Unit, comparand.Type) == (block.Unit, result.Type), comparand);
        Check.Null(value);
        Check.Argument((value.Unit, value.Type) == (block.Unit, result.Type), value);

        Order = order;
        Result = result;
        Address = address;
        Comparand = comparand;
        Value = value;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Address, Comparand, Value);
    }

    private protected override IReadOnlySet<Variable> GetWrites()
    {
        return CreateSet(Result);
    }

    private protected override IReadOnlySet<BasicBlock> GetTargets()
    {
        return EmptyBlocks;
    }

    public override string ToString()
    {
        return $"{Result} = cmpxchg {Order.ToIRString()} [{Address}], {Comparand}, {Value}";
    }
}
