namespace Vezel.Niru.Translation.Instructions;

public sealed class FExtendInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Variable Result { get; }

    public Variable Operand { get; }

    internal FExtendInstruction(BasicBlock block, Variable result, Variable operand)
        : base(block)
    {
        Check.Null(result);
        Check.Argument((result.Unit, result.Type) == (block.Unit, TypeId.Float64), result);
        Check.Null(operand);
        Check.Argument((operand.Unit, operand.Type) == (block.Unit, TypeId.Float32), operand);

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
        return $"{Result} = fext {Operand}";
    }
}
