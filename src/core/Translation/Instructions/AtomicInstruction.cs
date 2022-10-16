namespace Vezel.Niru.Translation.Instructions;

public sealed class AtomicInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => true;

    public AtomicOperation Operation { get; }

    public MemoryOrder Order { get; }

    public Variable Result { get; }

    public Variable Address { get; }

    public Variable Operand { get; }

    internal AtomicInstruction(
        BasicBlock block,
        AtomicOperation operation,
        MemoryOrder order,
        Variable result,
        Variable address,
        Variable operand)
        : base(block)
    {
        Check.Enum(operation);
        Check.Enum(order);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(address);
        Check.Argument((address.Unit, address.Type) == (block.Unit, TypeId.Int64), address);
        Check.Null(operand);
        Check.Argument((operand.Unit, operand.Type) == (block.Unit, result.Type), operand);

        Operation = operation;
        Order = order;
        Result = result;
        Address = address;
        Operand = operand;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Address, Operand);
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
        return $"{Result} = atomic {Operation.ToIRString()} {Order.ToIRString()} [{Address}], {Operand}";
    }
}
