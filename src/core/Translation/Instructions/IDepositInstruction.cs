namespace Vezel.Niru.Translation.Instructions;

public sealed class IDepositInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Range Range { get; }

    public Variable Result { get; }

    public Variable Value { get; }

    public Variable Field { get; }

    internal IDepositInstruction(BasicBlock block, Range range, Variable result, Variable value, Variable field)
        : base(block)
    {
        Check.Range(range, block.Unit.Translator.Machine.GetSize(result.Type) * 8);
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit && result.Type is TypeId.Int32 or TypeId.Int64, result);
        Check.Null(value);
        Check.Argument((value.Unit, value.Type) == (block.Unit, result.Type), value);
        Check.Null(field);
        Check.Argument((field.Unit, field.Type) == (block.Unit, result.Type), field);

        Range = range;
        Result = result;
        Value = value;
        Field = field;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Value, Field);
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
        return $"{Result} = ideposit {Range}, {Value}, {Field}";
    }
}
