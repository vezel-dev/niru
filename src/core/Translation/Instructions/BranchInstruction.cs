namespace Vezel.Niru.Translation.Instructions;

public sealed class BranchInstruction : Instruction
{
    public override bool IsTerminator => true;

    public override bool HasSideEffects => false;

    public Variable Condition { get; }

    public BasicBlock Left { get; }

    public BasicBlock Right { get; }

    internal BranchInstruction(BasicBlock block, Variable condition, BasicBlock left, BasicBlock right)
        : base(block)
    {
        Check.Null(condition);
        Check.Argument(condition.Unit == block.Unit && condition.Type is TypeId.Int32 or TypeId.Int64, condition);
        Check.Null(left);
        Check.Argument(left.Unit == block.Unit && !left.IsEntry, left);
        Check.Null(right);
        Check.Argument(right.Unit == block.Unit && !right.IsEntry, right);

        Condition = condition;
        Left = left;
        Right = right;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Condition);
    }

    private protected override IReadOnlySet<Variable> GetWrites()
    {
        return EmptyVariables;
    }

    private protected override IReadOnlySet<BasicBlock> GetTargets()
    {
        return CreateSet(Left, Right);
    }

    public override string ToString()
    {
        return $"br {Condition} ({Left} or {Right})";
    }
}
