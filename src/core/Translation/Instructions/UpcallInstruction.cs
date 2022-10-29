namespace Vezel.Niru.Translation.Instructions;

public sealed unsafe class UpcallInstruction : Instruction
{
    public override bool IsTerminator => false;

    public override bool HasSideEffects => Attributes.HasFlag(UpcallAttributes.HasSideEffects);

    public string Name { get; }

    public void* Function { get; }

    public UpcallAttributes Attributes { get; }

    public Variable? Result { get; }

    public IReadOnlyList<Variable> Arguments { get; }

    internal UpcallInstruction(
        BasicBlock block,
        string name,
        void* function,
        UpcallAttributes attributes,
        Variable? result,
        IEnumerable<Variable> arguments)
        : base(block)
    {
        Check.NullOrEmpty(name);
        Check.Null(function);
        Check.Null(arguments);
        Check.All(arguments, block.Unit, static (arg, unit) => arg?.Unit == unit);

        Name = name;
        Function = function;
        Attributes = attributes;
        Result = result;
        Arguments = arguments.ToArray();
    }

    private protected override IReadOnlySet<Variable> GetReads()
    {
        return Arguments.ToHashSet();
    }

    private protected override IReadOnlySet<Variable> GetWrites()
    {
        return Result != null ? CreateSet(Result) : EmptyVariables;
    }

    private protected override IReadOnlySet<BasicBlock> GetTargets()
    {
        return EmptyBlocks;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var culture = CultureInfo.InvariantCulture;

        if (Result != null)
            _ = sb.Append(culture, $"{Result} = ");

        _ = sb.Append(culture, $"upcall {Attributes.ToIRString()} {Name}@0x{(nuint)Function:x} (");

        for (var i = 0; i < Arguments.Count; i++)
        {
            _ = sb.Append(Arguments[i]);

            if (i != Arguments.Count - 1)
                _ = sb.Append(", ");
        }

        return sb.Append(')').ToString();
    }
}
