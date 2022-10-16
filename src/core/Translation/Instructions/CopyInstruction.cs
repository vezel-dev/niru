namespace Vezel.Niru.Translation.Instructions;

public sealed class CopyInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => false;

    public Variable Result { get; }

    public Variable Value { get; }

    internal CopyInstruction(BasicBlock block, Variable result, Variable value)
        : base(block)
    {
        Check.Null(result);
        Check.Argument(result.Unit == block.Unit, result);
        Check.Null(value);
        Check.Argument(
            value.Unit == block.Unit && (result.Type, value.Type) switch
            {
                (TypeId.Int32 or TypeId.Float32, TypeId.Int32 or TypeId.Float32) => true,
                (TypeId.Int64 or TypeId.Float64, TypeId.Int64 or TypeId.Float64) => true,
                (TypeId.Vector, TypeId.Vector) => true,
                _ => false,
            },
            value);

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
        return $"{Result} = copy {Value}";
    }
}
