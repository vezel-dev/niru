namespace Vezel.Niru.Translation.Instructions;

public sealed class StoreInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => true;

    public StoreWidth Width { get; }

    public MemoryOrder? Order { get; }

    public Variable Address { get; }

    public Variable Value { get; }

    internal StoreInstruction(BasicBlock block, StoreWidth width, MemoryOrder? order, Variable address, Variable value)
        : base(block)
    {
        Check.Enum(width);
        Check.Enum(order);
        Check.Null(address);
        Check.Argument((address.Unit, address.Type) == (block.Unit, TypeId.Int64), address);
        Check.Null(value);
        Check.Argument(
            value.Unit == block.Unit && (value.Type, width) switch
            {
                (TypeId.Int32, <= StoreWidth.Int32) => true,
                (TypeId.Int64, <= StoreWidth.Int64) => true,
                (TypeId.Float32, StoreWidth.Float32) => true,
                (TypeId.Float64, StoreWidth.Float64) => true,
                (TypeId.Vector, StoreWidth.Vector) => true,
                _ => false,
            },
            value);

        Width = width;
        Order = order;
        Address = address;
        Value = value;
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return CreateSet(Address, Value);
    }

    private protected override IReadOnlySet<Variable> GetWrites()
    {
        return EmptyVariables;
    }

    private protected override IReadOnlySet<BasicBlock> GetTargets()
    {
        return EmptyBlocks;
    }

    public override string ToString()
    {
        var culture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder()
            .Append(culture, $"store {Width.ToIRString()} ");

        if (Order is MemoryOrder order)
            _ = sb.Append(culture, $"{order.ToIRString()} ");

        return sb.Append(culture, $"[{Address}], {Value}").ToString();
    }
}
