namespace Vezel.Niru.Translation.Instructions;

public sealed class SExtractInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Range Range { get; }

    public Variable Result { get; }

    public Variable Value { get; }

    internal SExtractInstruction(BasicBlock block, Range range, Variable result, Variable value)
        : base(block)
    {
        Check.Range(range, block.Unit.Translator.Machine.GetSize(result.Type) * 8);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(value);
        Check.Argument((value.Unit, value.Type) == (block.Unit, result.Type), value);

        Range = range;
        Result = result;
        Value = value;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Value);
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
        return $"{Result} = sextract {Range}, {Value}";
    }
}
