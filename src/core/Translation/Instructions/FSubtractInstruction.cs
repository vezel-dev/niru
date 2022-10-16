namespace Vezel.Niru.Translation.Instructions;

public sealed class FSubtractInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public MidpointRounding? Rounding { get; }

    public Variable Result { get; }

    public Variable Left { get; }

    public Variable Right { get; }

    internal FSubtractInstruction(
        BasicBlock block, MidpointRounding? rounding, Variable result, Variable left, Variable right)
        : base(block)
    {
        Check.Enum(rounding);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Float32 or TypeId.Float64, result);
        Check.Null(left);
        Check.Argument((left.Unit, left.Type) == (block.Unit, result.Type), left);
        Check.Null(right);
        Check.Argument((right.Unit, right.Type) == (block.Unit, result.Type), right);

        Rounding = rounding;
        Result = result;
        Left = left;
        Right = right;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Left, Right);
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
        return $"{Result} = fsub {Left}, {Right}";
    }
}
