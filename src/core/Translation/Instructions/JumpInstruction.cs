namespace Vezel.Niru.Translation.Instructions;

public sealed class JumpInstruction : Instruction
{
    public override bool IsTerminator => true;

    public override bool HasSideEffects => false;

    public BasicBlock Target { get; }

    internal JumpInstruction(BasicBlock block, BasicBlock target)
        : base(block)
    {
        Check.Null(target);
        Check.Argument(target.Unit == block.Unit && !target.IsEntry, target);

        Target = target;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return EmptyVariables;
    }

    private protected override IReadOnlySet<Variable> GetWrites()
    {
        return EmptyVariables;
    }

    private protected override IReadOnlySet<BasicBlock> GetTargets()
    {
        return new HashSet<BasicBlock>(1)
        {
            Target,
        };
    }

    public override string ToString()
    {
        return $"jmp ({Target})";
    }
}
