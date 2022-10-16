namespace Vezel.Niru.Translation.Instructions;

internal static class InstructionExtensions
{
    public static string ToIRString(this AtomicOperation operation)
    {
        return operation switch
        {
            AtomicOperation.Exchange => "xchg",
            AtomicOperation.Add => "add",
            AtomicOperation.Subtract => "sub",
            AtomicOperation.And => "and",
            AtomicOperation.Or => "or",
            AtomicOperation.Xor => "xor",
            AtomicOperation.SignedMin => "smin",
            AtomicOperation.UnsignedMin => "umin",
            AtomicOperation.SignedMax => "smax",
            AtomicOperation.UnsignedMax => "umax",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this FloatComparison comparison)
    {
        return comparison switch
        {
            FloatComparison.Ordered => "ord",
            FloatComparison.OrderedEqual => "oeq",
            FloatComparison.OrderedNotEqual => "one",
            FloatComparison.OrderedGreater => "ogt",
            FloatComparison.OrderedGreaterOrEqual => "oge",
            FloatComparison.OrderedLess => "olt",
            FloatComparison.OrderedLessOrEqual => "ole",
            FloatComparison.Unordered => "uno",
            FloatComparison.UnorderedEqual => "ueq",
            FloatComparison.UnorderedNotEqual => "une",
            FloatComparison.UnorderedGreater => "ugt",
            FloatComparison.UnorderedGreaterOrEqual => "uge",
            FloatComparison.UnorderedLess => "ult",
            FloatComparison.UnorderedLessOrEqual => "ule",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this IntegerComparison comparison)
    {
        return comparison switch
        {
            IntegerComparison.Equal => "eql",
            IntegerComparison.NotEqual => "neq",
            IntegerComparison.UnsignedGreater => "ugt",
            IntegerComparison.UnsignedGreaterOrEqual => "uge",
            IntegerComparison.UnsignedLess => "ult",
            IntegerComparison.UnsignedLessOrEqual => "ule",
            IntegerComparison.SignedGreater => "sgt",
            IntegerComparison.SignedGreaterOrEqual => "sge",
            IntegerComparison.SignedLess => "slt",
            IntegerComparison.SignedLessOrEqual => "sle",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this LoadWidth width)
    {
        return width switch
        {
            LoadWidth.UInt8 => "u8",
            LoadWidth.SInt8 => "s8",
            LoadWidth.UInt16 => "u16",
            LoadWidth.SInt16 => "s16",
            LoadWidth.UInt32 => "u32",
            LoadWidth.SInt32 => "s32",
            LoadWidth.UInt64 => "u64",
            LoadWidth.SInt64 => "s64",
            LoadWidth.Float32 => "f32",
            LoadWidth.Float64 => "f64",
            LoadWidth.Vector => "vec",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this MemoryOrder order)
    {
        return order switch
        {
            MemoryOrder.Relaxed => "rlx",
            MemoryOrder.Acquire => "acq",
            MemoryOrder.Release => "rel",
            MemoryOrder.Sequential => "seq",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this MidpointRounding rounding)
    {
        return rounding switch
        {
            MidpointRounding.ToEven => "rne",
            MidpointRounding.AwayFromZero => "rmm",
            MidpointRounding.ToZero => "rtz",
            MidpointRounding.ToNegativeInfinity => "rdn",
            MidpointRounding.ToPositiveInfinity => "rup",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this StoreWidth width)
    {
        return width switch
        {
            StoreWidth.Int8 => "i8",
            StoreWidth.Int16 => "i16",
            StoreWidth.Int32 => "i32",
            StoreWidth.Int64 => "i64",
            StoreWidth.Float32 => "f32",
            StoreWidth.Float64 => "f64",
            StoreWidth.Vector => "vec",
            _ => throw new UnreachableException(),
        };
    }

    public static string ToIRString(this UpcallAttributes attributes)
    {
        var attrs = new List<string>(sizeof(UpcallAttributes) * 8);

        if (attributes.HasFlag(UpcallAttributes.HasSideEffects))
            attrs.Add("eff");

        if (attributes.HasFlag(UpcallAttributes.ReadsContext))
            attrs.Add("rd");

        if (attributes.HasFlag(UpcallAttributes.WritesContext))
            attrs.Add("wr");

        return attrs.Count != 0 ? string.Join('|', attrs) : "pure";
    }
}
