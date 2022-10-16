namespace Vezel.Niru.Translation;

public sealed class TranslationUnit
{
    public CodeTranslator Translator { get; }

    public IReadOnlyDictionary<RegisterInfo, RegisterVariable> Registers => _registers;

    public IReadOnlyDictionary<int, TemporaryVariable> Temporaries => _temporaries;

    public IReadOnlyDictionary<int, BasicBlock> Blocks => _blocks;

    public BasicBlock Entry { get; }

    private readonly SortedDictionary<RegisterInfo, RegisterVariable> _registers = new(_registerComparer);

    private static readonly Comparer<RegisterInfo> _registerComparer =
        Comparer<RegisterInfo>.Create((x, y) =>
        {
            var cmp = Comparer<RegisterBank>.Default.Compare(x.Bank, y.Bank);

            return cmp != 0 ? cmp : x.Id.CompareTo(y.Id);
        });

    private readonly SortedDictionary<int, TemporaryVariable> _temporaries = new();

    private readonly SortedDictionary<int, BasicBlock> _blocks = new();

    private int _blockId;

    private int _temporaryId;

    internal TranslationUnit(CodeTranslator translator)
    {
        Translator = translator;
        Entry = CreateBlock();
    }

    public BasicBlock CreateBlock()
    {
        var id = _blockId++;
        var block = new BasicBlock(this, id);

        _blocks.Add(id, block);

        return block;
    }

    internal void RemoveBlock(BasicBlock block)
    {
        Check.Operation(_blocks.Remove(block.Id));
    }

    public RegisterVariable GetRegister(RegisterInfo register)
    {
        Check.Null(register);
        Check.Argument(register.Machine == Translator.Machine, register);

        if (!_registers.TryGetValue(register, out var variable))
        {
            variable = new RegisterVariable(this, register);

            _registers.Add(register, variable);
        }

        return variable;
    }

    public TemporaryVariable CreateTemporary(TypeId type)
    {
        Check.Enum(type);

        var id = _temporaryId++;
        var variable = new TemporaryVariable(this, type, id);

        _temporaries.Add(id, variable);

        return variable;
    }

    internal void RemoveTemporary(TemporaryVariable variable)
    {
        Check.Operation(_temporaries.Remove(variable.Id));
    }

    public override string ToString()
    {
        var sb = new StringBuilder($"unit")
            .AppendLine()
            .AppendLine("{");

        void WriteList<T>(IReadOnlyList<T> items)
        {
            _ = sb.Append('[');

            var count = items.Count;

            for (var i = 0; i < count; i++)
            {
                _ = sb.Append(items[i]);

                if (i != count - 1)
                    _ = sb.Append(", ");
            }

            _ = sb.Append(']');
        }

        var culture = CultureInfo.InvariantCulture;

        if (_temporaries.Count != 0)
        {
            foreach (var (_, temp) in _temporaries)
            {
                _ = sb.Append(culture, $"  var {temp} : {temp.Type.ToIRString()}; // W ({temp.Writes.Count}): ");

                WriteList(temp.Writes.Select(i => i.Block).Distinct().OrderBy(b => b.Id).ToArray());

                _ = sb.Append(culture, $" R ({temp.Reads.Count}): ");

                WriteList(temp.Reads.Select(i => i.Block).Distinct().OrderBy(b => b.Id).ToArray());

                _ = sb.AppendLine();
            }

            _ = sb.AppendLine();
        }

        var i = 0;

        foreach (var (_, block) in _blocks)
        {
            _ = sb.Append(culture, $"  block {block} // P ({block.Predecessors.Count}): ");

            WriteList(block.Predecessors.ToArray());

            _ = sb.Append(culture, $" S ({block.Successors.Count}): ");

            WriteList(block.Successors.ToArray());

            _ = sb
                .AppendLine()
                .AppendLine("  {");

            foreach (var insn in block.Instructions)
                _ = sb.AppendLine(culture, $"    {insn};");

            _ = sb.AppendLine("  }");

            if (i++ != _blocks.Count - 1)
                _ = sb.AppendLine();
        }

        return sb.Append('}').ToString();
    }
}
