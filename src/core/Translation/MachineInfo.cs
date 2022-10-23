namespace Vezel.Niru.Translation;

public abstract class MachineInfo
{
    public abstract int WordSize { get; }

    public abstract int VectorSize { get; }

    public TypeId WordType => WordSize switch
    {
        32 => TypeId.Int32,
        64 => TypeId.Int64,
        _ => throw new InvalidOperationException(),
    };

    public RegisterInfo PC =>
        LazyInitializer.EnsureInitialized(
            ref _pc,
            ref _lock,
            () => new(this, RegisterBank.Integer, -1, WordType, "pc"));

    public IReadOnlyDictionary<int, RegisterInfo> SystemRegisters =>
        LazyInitializer.EnsureInitialized(
            ref _systemRegisters,
            ref _lock,
            () =>
            {
                var regs = new Dictionary<int, RegisterInfo>();

                CreateSystemRegisters(new((id, type, name) =>
                {
                    Check.Enum(type);
                    Check.NullOrEmpty(name);
                    Check.Argument(regs.TryAdd(id, new(this, RegisterBank.System, id, type, name)), id);
                }));

                return regs.AsReadOnly();
            });

    public IReadOnlyList<RegisterInfo> IntegerRegisters =>
        LazyInitializer.EnsureInitialized(
            ref _integerRegisters,
            ref _lock,
            () =>
            {
                var i = 0;
                var regs = new List<RegisterInfo>();

                CreateIntegerRegisters(new(name =>
                {
                    Check.NullOrEmpty(name);

                    regs.Add(new(this, RegisterBank.Integer, i++, WordType, name));
                }));

                return regs.AsReadOnly();
            });

    public IReadOnlyList<RegisterInfo> FloatRegisters =>
        LazyInitializer.EnsureInitialized(
            ref _floatRegisters,
            ref _lock,
            () =>
            {
                var i = 0;
                var regs = new List<RegisterInfo>();

                CreateFloatRegisters(new((type, name) =>
                {
                    Check.Range(type is TypeId.Float32 or TypeId.Float64, type);
                    Check.NullOrEmpty(name);

                    regs.Add(new(this, RegisterBank.Float, i++, type, name));
                }));

                return regs.AsReadOnly();
            });

    public IReadOnlyList<RegisterInfo> VectorRegisters =>
        LazyInitializer.EnsureInitialized(
            ref _vectorRegisters,
            ref _lock,
            () =>
            {
                var i = 0;
                var regs = new List<RegisterInfo>();

                CreateVectorRegisters(new(name =>
                {
                    Check.NullOrEmpty(name);

                    regs.Add(new(this, RegisterBank.Vector, i++, TypeId.Vector, name));
                }));

                return regs.AsReadOnly();
            });

    private object? _lock;

    private RegisterInfo? _pc;

    private IReadOnlyDictionary<int, RegisterInfo>? _systemRegisters;

    private IReadOnlyList<RegisterInfo>? _integerRegisters;

    private IReadOnlyList<RegisterInfo>? _floatRegisters;

    private IReadOnlyList<RegisterInfo>? _vectorRegisters;

    public int GetSize(TypeId type)
    {
        return type switch
        {
            TypeId.Int32 => sizeof(int),
            TypeId.Int64 => sizeof(long),
            TypeId.Float32 => sizeof(float),
            TypeId.Float64 => sizeof(double),
            TypeId.Vector => VectorSize,
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }

    protected abstract void CreateSystemRegisters(scoped ScopedAction<int, TypeId, string> creator);

    protected abstract void CreateIntegerRegisters(scoped ScopedAction<string> creator);

    protected abstract void CreateFloatRegisters(scoped ScopedAction<TypeId, string> creator);

    protected abstract void CreateVectorRegisters(scoped ScopedAction<string> creator);
}
