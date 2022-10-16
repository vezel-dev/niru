using Vezel.Niru.Translation.Instructions;

namespace Vezel.Niru.Translation;

public sealed unsafe class CodeEmitter : IDisposable
{
    public BasicBlock Block { get; }

    public IReadOnlyList<Instruction> Instructions => _instructions;

    private readonly List<Instruction> _instructions = new();

    private readonly Action<IReadOnlyList<Instruction>> _action;

    private bool _terminator;

    private bool _committed;

    internal CodeEmitter(BasicBlock block, Action<IReadOnlyList<Instruction>> action)
    {
        Block = block;
        _action = action;
    }

    public void Dispose()
    {
        if (_instructions.Count == 0 || _committed)
            return;

        Check.Operation(!_terminator || Block.Terminator == null);

        _action(_instructions);

        _committed = true;
    }

    private void Emit(Instruction instruction)
    {
        Check.Operation(!_terminator);

        if (instruction.IsTerminator)
            _terminator = true;

        _instructions.Add(instruction);
    }

    public void Atomic(
        AtomicOperation operation, MemoryOrder order, Variable result, Variable address, Variable operand)
    {
        Emit(new AtomicInstruction(Block, operation, order, result, address, operand));
    }

    public void Branch(Variable condition, BasicBlock left, BasicBlock right)
    {
        Emit(new BranchInstruction(Block, condition, left, right));
    }

    public void CompareExchange(
        MemoryOrder order, Variable result, Variable address, Variable comparand, Variable value)
    {
        Emit(new CompareExchangeInstruction(Block, order, result, address, comparand, value));
    }

    public void Constant(Variable result, object value)
    {
        Emit(new ConstantInstruction(Block, result, value));
    }

    public void Copy(Variable result, Variable value)
    {
        Emit(new CopyInstruction(Block, result, value));
    }

    public void FAbs(Variable result, Variable operand)
    {
        Emit(new FAbsInstruction(Block, result, operand));
    }

    public void FAdd(Variable result, Variable left, Variable right)
    {
        Emit(new FAddInstruction(Block, null, result, left, right));
    }

    public void FAdd(MidpointRounding rounding, Variable result, Variable left, Variable right)
    {
        Emit(new FAddInstruction(Block, rounding, result, left, right));
    }

    public void FCompare(FloatComparison comparison, Variable result, Variable left, Variable right)
    {
        Emit(new FCompareInstruction(Block, comparison, result, left, right));
    }

    public void FConvertS(Variable result, Variable operand)
    {
        Emit(new FConvertSInstruction(Block, null, result, operand));
    }

    public void FConvertS(MidpointRounding rounding, Variable result, Variable operand)
    {
        Emit(new FConvertSInstruction(Block, rounding, result, operand));
    }

    public void FConvertU(Variable result, Variable operand)
    {
        Emit(new FConvertUInstruction(Block, null, result, operand));
    }

    public void FConvertU(MidpointRounding rounding, Variable result, Variable operand)
    {
        Emit(new FConvertUInstruction(Block, rounding, result, operand));
    }

    public void FDivide(Variable result, Variable left, Variable right)
    {
        Emit(new FDivideInstruction(Block, null, result, left, right));
    }

    public void FDivide(MidpointRounding rounding, Variable result, Variable left, Variable right)
    {
        Emit(new FDivideInstruction(Block, rounding, result, left, right));
    }

    public void Fence(MemoryOrder order)
    {
        Emit(new FenceInstruction(Block, order));
    }

    public void FExtend(Variable result, Variable operand)
    {
        Emit(new FExtendInstruction(Block, result, operand));
    }

    public void FMin(Variable result, Variable left, Variable right)
    {
        Emit(new FMinInstruction(Block, result, left, right));
    }

    public void FMax(Variable result, Variable left, Variable right)
    {
        Emit(new FMaxInstruction(Block, result, left, right));
    }

    public void FMultiply(Variable result, Variable left, Variable right)
    {
        Emit(new FMultiplyInstruction(Block, null, result, left, right));
    }

    public void FMultiply(MidpointRounding rounding, Variable result, Variable left, Variable right)
    {
        Emit(new FMultiplyInstruction(Block, rounding, result, left, right));
    }

    public void FNegate(Variable result, Variable operand)
    {
        Emit(new FNegateInstruction(Block, result, operand));
    }

    public void FSqrt(Variable result, Variable operand)
    {
        Emit(new FSqrtInstruction(Block, null, result, operand));
    }

    public void FSqrt(MidpointRounding rounding, Variable result, Variable operand)
    {
        Emit(new FSqrtInstruction(Block, rounding, result, operand));
    }

    public void FSubtract(Variable result, Variable left, Variable right)
    {
        Emit(new FSubtractInstruction(Block, null, result, left, right));
    }

    public void FSubtract(MidpointRounding rounding, Variable result, Variable left, Variable right)
    {
        Emit(new FSubtractInstruction(Block, rounding, result, left, right));
    }

    public void FTruncate(Variable result, Variable operand)
    {
        Emit(new FTruncateInstruction(Block, result, operand));
    }

    public void IAdd(Variable result, Variable left, Variable right)
    {
        Emit(new IAddInstruction(Block, result, left, right));
    }

    public void IAnd(Variable result, Variable left, Variable right)
    {
        Emit(new IAndInstruction(Block, result, left, right));
    }

    public void ICompare(IntegerComparison comparison, Variable result, Variable left, Variable right)
    {
        Emit(new ICompareInstruction(Block, comparison, result, left, right));
    }

    public void IDeposit(Range range, Variable result, Variable value, Variable field)
    {
        Emit(new IDepositInstruction(Block, range, result, value, field));
    }

    public void ILeadingZeroCount(Variable result, Variable value, Variable fallback)
    {
        Emit(new ILeadingZeroCountInstruction(Block, result, value, fallback));
    }

    public void IMultiply(Variable result, Variable left, Variable right)
    {
        Emit(new IMultiplyInstruction(Block, result, left, right));
    }

    public void INegate(Variable result, Variable operand)
    {
        Emit(new INegateInstruction(Block, result, operand));
    }

    public void INot(Variable result, Variable operand)
    {
        Emit(new INotInstruction(Block, result, operand));
    }

    public void IOr(Variable result, Variable left, Variable right)
    {
        Emit(new IOrInstruction(Block, result, left, right));
    }

    public void IPopCount(Variable result, Variable operand)
    {
        Emit(new IPopCountInstruction(Block, result, operand));
    }

    public void IRotateLeft(Variable result, Variable left, Variable right)
    {
        Emit(new IRotateLeftInstruction(Block, result, left, right));
    }

    public void IRotateRight(Variable result, Variable left, Variable right)
    {
        Emit(new IRotateRightInstruction(Block, result, left, right));
    }

    public void IShiftLeft(Variable result, Variable left, Variable right)
    {
        Emit(new IShiftLeftInstruction(Block, result, left, right));
    }

    public void ISubtract(Variable result, Variable left, Variable right)
    {
        Emit(new ISubtractInstruction(Block, result, left, right));
    }

    public void ITrailingZeroCount(Variable result, Variable value, Variable fallback)
    {
        Emit(new ITrailingZeroCountInstruction(Block, result, value, fallback));
    }

    public void ITruncate(Variable result, Variable operand)
    {
        Emit(new ITruncateInstruction(Block, result, operand));
    }

    public void IXor(Variable result, Variable left, Variable right)
    {
        Emit(new IXorInstruction(Block, result, left, right));
    }

    public void Jump(BasicBlock target)
    {
        Emit(new JumpInstruction(Block, target));
    }

    public void Load(LoadWidth width, Variable result, Variable address)
    {
        Emit(new LoadInstruction(Block, width, null, result, address));
    }

    public void Load(LoadWidth width, MemoryOrder order, Variable result, Variable address)
    {
        Emit(new LoadInstruction(Block, width, order, result, address));
    }

    public void SConvertF(Variable result, Variable operand)
    {
        Emit(new SConvertFInstruction(Block, null, result, operand));
    }

    public void SConvertF(MidpointRounding rounding, Variable result, Variable operand)
    {
        Emit(new SConvertFInstruction(Block, rounding, result, operand));
    }

    public void SDivide(Variable result, Variable left, Variable right)
    {
        Emit(new SDivideInstruction(Block, result, left, right));
    }

    public void SExtend(Variable result, Variable operand)
    {
        Emit(new SExtendInstruction(Block, result, operand));
    }

    public void SExtract(Range range, Variable result, Variable value)
    {
        Emit(new SExtractInstruction(Block, range, result, value));
    }

    public void SRemainder(Variable result, Variable left, Variable right)
    {
        Emit(new SRemainderInstruction(Block, result, left, right));
    }

    public void SShiftRight(Variable result, Variable left, Variable right)
    {
        Emit(new SShiftRightInstruction(Block, result, left, right));
    }

    public void Store(StoreWidth width, Variable address, Variable value)
    {
        Emit(new StoreInstruction(Block, width, null, address, value));
    }

    public void Store(StoreWidth width, MemoryOrder order, Variable address, Variable value)
    {
        Emit(new StoreInstruction(Block, width, order, address, value));
    }

    public void UConvertF(Variable result, Variable operand)
    {
        Emit(new UConvertFInstruction(Block, null, result, operand));
    }

    public void UConvertF(MidpointRounding rounding, Variable result, Variable operand)
    {
        Emit(new UConvertFInstruction(Block, rounding, result, operand));
    }

    public void UDivide(Variable result, Variable left, Variable right)
    {
        Emit(new UDivideInstruction(Block, result, left, right));
    }

    public void UExtend(Variable result, Variable operand)
    {
        Emit(new UExtendInstruction(Block, result, operand));
    }

    public void UExtract(Range range, Variable result, Variable value)
    {
        Emit(new UExtractInstruction(Block, range, result, value));
    }

    public void Upcall(string name, void* function, UpcallAttributes attributes, IEnumerable<Variable> arguments)
    {
        Emit(new UpcallInstruction(Block, name, function, attributes, null, arguments));
    }

    public void Upcall(
        string name, void* function, UpcallAttributes attributes, Variable result, IEnumerable<Variable> arguments)
    {
        Emit(new UpcallInstruction(Block, name, function, attributes, result, arguments));
    }

    public void URemainder(Variable result, Variable left, Variable right)
    {
        Emit(new URemainderInstruction(Block, result, left, right));
    }

    public void UShiftRight(Variable result, Variable left, Variable right)
    {
        Emit(new UShiftRightInstruction(Block, result, left, right));
    }

    public void Yield(Variable address)
    {
        Emit(new YieldInstruction(Block, address));
    }
}
