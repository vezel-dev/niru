namespace Vezel.Niru.Translation.Instructions;

public sealed class FCompareInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public FloatComparison Comparison { get; }

    public Variable Result { get; }

    public Variable Left { get; }

    public Variable Right { get; }

    internal FCompareInstruction(
        BasicBlock block, FloatComparison comparison, Variable result, Variable left, Variable right)
        : base(block)
    {
        Check.Enum(comparison);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(left);
        Check.Argument(left.Unit == block.Unit && left.Type is TypeId.Float32 or TypeId.Float64, left);
        Check.Null(right);
        Check.Argument((right.Unit, right.Type) == (block.Unit, left.Type), right);

        Comparison = comparison;
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
        return $"{Result} = fcmp {Comparison.ToIRString()} {Left}, {Right}";
    }
}
