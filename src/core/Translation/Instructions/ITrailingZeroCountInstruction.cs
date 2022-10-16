namespace Vezel.Niru.Translation.Instructions;

public sealed class ITrailingZeroCountInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Variable Result { get; }

    public Variable Value { get; }

    public Variable Fallback { get; }

    internal ITrailingZeroCountInstruction(BasicBlock block, Variable result, Variable value, Variable fallback)
        : base(block)
    {
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(value);
        Check.Argument((value.Unit, value.Type) == (block.Unit, result.Type), value);
        Check.Null(fallback);
        Check.Argument((fallback.Unit, fallback.Type) == (block.Unit, result.Type), fallback);

        Result = result;
        Value = value;
        Fallback = fallback;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Value, Fallback);
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
        return $"{Result} = itzcount {Value}, {Fallback}";
    }
}
