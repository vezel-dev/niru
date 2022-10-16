namespace Vezel.Niru.Translation.Instructions;

public sealed class ConstantInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Variable Result { get; }

    public object Value { get; }

    internal ConstantInstruction(BasicBlock block, Variable result, object value)
        : base(block)
    {
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type != TypeId.Vector, result);
        Check.Argument(
            (result.Type, value) switch
            {
                (TypeId.Int32, uint or int) => true,
                (TypeId.Int64, ulong or long) => true,
                (TypeId.Float32, float) => true,
                (TypeId.Float64, double) => true,
                _ => false,
            },
            value);

        Result = result;
        Value = value;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return EmptyVariables;
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
        return $"{Result} = const {Value}";
    }
}
