namespace Vezel.Niru.Translation.Instructions;

public sealed class IAndInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Variable Result { get; }

    public Variable Left { get; }

    public Variable Right { get; }

    internal IAndInstruction(BasicBlock block, Variable result, Variable left, Variable right)
        : base(block)
    {
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(left);
        Check.Argument((left.Unit, left.Type) == (block.Unit, result.Type), left);
        Check.Null(right);
        Check.Argument((right.Unit, right.Type) == (block.Unit, result.Type), right);

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
        return $"{Result} = iand {Left}, {Right}";
    }
}
