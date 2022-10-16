namespace Vezel.Niru.Translation.Instructions;

public sealed class LoadInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => true;

    public LoadWidth Width { get; }

    public MemoryOrder? Order { get; }

    public Variable Result { get; }

    public Variable Address { get; }

    internal LoadInstruction(BasicBlock block, LoadWidth width, MemoryOrder? order, Variable result, Variable address)
        : base(block)
    {
        Check.Enum(width);
        Check.Enum(order);
        Check.Null(result);
        Check.Argument(
            result.Unit == block.Unit && (result.Type, width) switch
            {
                (TypeId.Int32, <= LoadWidth.SInt32) => true,
                (TypeId.Int64, <= LoadWidth.SInt64) => true,
                (TypeId.Float32, LoadWidth.Float32) => true,
                (TypeId.Float64, LoadWidth.Float64) => true,
                (TypeId.Vector, LoadWidth.Vector) => true,
                _ => false,
            },
            result);
        Check.Null(address);
        Check.Argument((address.Unit, address.Type) == (block.Unit, TypeId.Int64), address);

        Width = width;
        Order = order;
        Result = result;
        Address = address;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Address);
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
        var culture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder()
            .Append(culture, $"{Result} = load {Width.ToIRString()} ");

        if (Order is MemoryOrder order)
            _ = sb.Append(culture, $"{order.ToIRString()} ");

        return sb.Append(culture, $"[{Address}]").ToString();
    }
}
