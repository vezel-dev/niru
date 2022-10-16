namespace Vezel.Niru.Translation.Instructions;

public sealed class FSqrtInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public MidpointRounding? Rounding { get; }

    public Variable Result { get; }

    public Variable Operand { get; }

    internal FSqrtInstruction(BasicBlock block, MidpointRounding? rounding, Variable result, Variable operand)
        : base(block)
    {
        Check.Enum(rounding);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Float32 or TypeId.Float64, result);
        Check.Null(operand);
        Check.Argument((operand.Unit, operand.Type) == (block.Unit, result.Type), operand);

        Rounding = rounding;
        Result = result;
        Operand = operand;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Operand);
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
        return $"{Result} = fsqrt {Operand}";
    }
}
