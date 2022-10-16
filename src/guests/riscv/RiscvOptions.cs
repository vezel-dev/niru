namespace Vezel.Niru.Guests.Riscv;

public sealed class RiscvOptions
{
    public int RegisterLength { get; private set; }

    public bool ExtensionA { get; private set; } = true;

    public bool ExtensionC { get; private set; } = true;

    public bool ExtensionD { get; private set; } = true;

    public bool ExtensionE { get; private set; }

    public bool ExtensionF { get; private set; } = true;

    public bool ExtensionH { get; private set; } = true;

    public bool ExtensionM { get; private set; } = true;

    public bool ExtensionS { get; private set; } = true;

    public RiscvPagingMode PagingMode { get; private set; }

    public bool ExtensionU { get; private set; } = true;

    public bool ExtensionV { get; private set; } = true;

    public int ElementLength { get; private set; } = 64;

    public int VectorLength { get; private set; } = 128;

    public bool ExtensionZdinx { get; private set; }

    public bool ExtensionZfh { get; private set; } = true;

    public bool ExtensionZfhmin { get; private set; } = true;

    public bool ExtensionZfinx { get; private set; }

    public bool ExtensionZhinx { get; private set; }

    public bool ExtensionZhinxmin { get; private set; }

    public bool ExtensionZicntr { get; private set; } = true;

    public bool ExtensionZicsr { get; private set; } = true;

    public bool ExtensionZifencei { get; private set; } = true;

    public bool ExtensionZihintpause { get; private set; } = true;

    public bool ExtensionZihpm { get; private set; } = true;

    public bool ExtensionZmmul { get; private set; } = true;

    public bool ExtensionSvinval { get; private set; } = true;

    public bool ExtensionSvnapot { get; private set; } = true;

    public bool ExtensionSvpbmt { get; private set; } = true;

    private RiscvOptions()
    {
    }

    public RiscvOptions(int xlen)
    {
        Check.Range(xlen is 32 or 64, xlen);

        RegisterLength = xlen;
        PagingMode = xlen == 64 ? RiscvPagingMode.Sv57 : RiscvPagingMode.Sv32;
    }

    public RiscvOptions Clone()
    {
        return new()
        {
            RegisterLength = RegisterLength,
            ExtensionA = ExtensionA,
            ExtensionC = ExtensionC,
            ExtensionD = ExtensionD,
            ExtensionE = ExtensionE,
            ExtensionF = ExtensionF,
            ExtensionH = ExtensionH,
            ExtensionM = ExtensionM,
            ExtensionS = ExtensionS,
            ExtensionU = ExtensionU,
            ExtensionV = ExtensionV,
            ExtensionZdinx = ExtensionZdinx,
            ExtensionZfh = ExtensionZfh,
            ExtensionZfhmin = ExtensionZfhmin,
            ExtensionZicntr = ExtensionZicntr,
            ExtensionZicsr = ExtensionZicsr,
            ExtensionZifencei = ExtensionZifencei,
            ExtensionZihintpause = ExtensionZihintpause,
            ExtensionZihpm = ExtensionZihpm,
            ExtensionZmmul = ExtensionZmmul,
            ExtensionSvinval = ExtensionSvinval,
            ExtensionSvnapot = ExtensionSvnapot,
            ExtensionSvpbmt = ExtensionSvpbmt,
        };
    }

    public RiscvOptions WithRegisterLength(int xlen)
    {
        Check.Range(xlen is 32 or 64, xlen);

        var opts = Clone();

        opts.RegisterLength = xlen;

        return opts;
    }

    public RiscvOptions WithExtensionA(bool value)
    {
        var opts = Clone();

        opts.ExtensionA = value;

        return opts;
    }

    public RiscvOptions WithExtensionC(bool value)
    {
        var opts = Clone();

        opts.ExtensionC = value;

        return opts;
    }

    public RiscvOptions WithExtensionD(bool value)
    {
        var opts = Clone();

        opts.ExtensionD = value;

        return opts;
    }

    public RiscvOptions WithExtensionE(bool value)
    {
        var opts = Clone();

        opts.ExtensionE = value;

        return opts;
    }

    public RiscvOptions WithExtensionF(bool value)
    {
        var opts = Clone();

        opts.ExtensionF = value;

        return opts;
    }

    public RiscvOptions WithExtensionH(bool value)
    {
        var opts = Clone();

        opts.ExtensionH = value;

        return opts;
    }

    public RiscvOptions WithExtensionM(bool value)
    {
        var opts = Clone();

        opts.ExtensionM = value;

        return opts;
    }

    public RiscvOptions WithExtensionS(bool value)
    {
        var opts = Clone();

        opts.ExtensionS = value;

        return opts;
    }

    public RiscvOptions WithPagingMode(RiscvPagingMode mode)
    {
        Check.Enum(mode);

        var opts = Clone();

        opts.PagingMode = mode;

        return opts;
    }

    public RiscvOptions WithExtensionU(bool value)
    {
        var opts = Clone();

        opts.ExtensionU = value;

        return opts;
    }

    public RiscvOptions WithExtensionV(bool value)
    {
        var opts = Clone();

        opts.ExtensionV = value;

        return opts;
    }

    public RiscvOptions WithVectorParameters(int elen, int vlen)
    {
        Check.Range(elen is 8 or 16 or 32 or 64, elen);
        Check.Range(vlen >= elen && vlen <= ushort.MaxValue + 1 && BitOperations.IsPow2(vlen), vlen);

        var opts = Clone();

        opts.ElementLength = elen;
        opts.VectorLength = vlen;

        return opts;
    }

    public RiscvOptions WithExtensionZdinx(bool value)
    {
        var opts = Clone();

        opts.ExtensionZdinx = value;

        return opts;
    }

    public RiscvOptions WithExtensionZfh(bool value)
    {
        var opts = Clone();

        opts.ExtensionZfh = value;

        return opts;
    }

    public RiscvOptions WithExtensionZfhmin(bool value)
    {
        var opts = Clone();

        opts.ExtensionZfhmin = value;

        return opts;
    }

    public RiscvOptions WithExtensionZicntr(bool value)
    {
        var opts = Clone();

        opts.ExtensionZicntr = value;

        return opts;
    }

    public RiscvOptions WithExtensionZicsr(bool value)
    {
        var opts = Clone();

        opts.ExtensionZicsr = value;

        return opts;
    }

    public RiscvOptions WithExtensionZifencei(bool value)
    {
        var opts = Clone();

        opts.ExtensionZifencei = value;

        return opts;
    }

    public RiscvOptions WithExtensionZihintpause(bool value)
    {
        var opts = Clone();

        opts.ExtensionZihintpause = value;

        return opts;
    }

    public RiscvOptions WithExtensionZihpm(bool value)
    {
        var opts = Clone();

        opts.ExtensionZihpm = value;

        return opts;
    }

    public RiscvOptions WithExtensionZmmul(bool value)
    {
        var opts = Clone();

        opts.ExtensionZmmul = value;

        return opts;
    }

    public RiscvOptions WithExtensionSvinval(bool value)
    {
        var opts = Clone();

        opts.ExtensionSvinval = value;

        return opts;
    }

    public RiscvOptions WithExtensionSvnapot(bool value)
    {
        var opts = Clone();

        opts.ExtensionSvnapot = value;

        return opts;
    }

    public RiscvOptions WithExtensionSvpbmt(bool value)
    {
        var opts = Clone();

        opts.ExtensionSvpbmt = value;

        return opts;
    }

    internal RiscvOptions Normalize()
    {
        var opts = Clone();

        if (opts.ExtensionV)
            opts.ExtensionD = true;

        if (opts.ExtensionZfh)
            opts.ExtensionZfhmin = true;

        if (opts.ExtensionD || opts.ExtensionZfhmin)
            opts.ExtensionF = true;

        if (opts.ExtensionZhinx)
            opts.ExtensionZhinxmin = true;

        if (opts.ExtensionZdinx || opts.ExtensionZhinxmin)
            opts.ExtensionZfinx = true;

        if (opts.ExtensionF || opts.ExtensionZicntr || opts.ExtensionZihpm)
            opts.ExtensionZicsr = true;

        if (opts.ExtensionM)
            opts.ExtensionZmmul = true;

        if (opts.ExtensionH || opts.ExtensionSvinval || opts.ExtensionSvnapot || opts.ExtensionSvpbmt)
            opts.ExtensionS = true;

        if (opts.ExtensionS)
            opts.ExtensionU = true;

        Check.Operation(!(opts.ExtensionH && opts.ExtensionE));
        Check.Operation(
            (opts.RegisterLength, opts.PagingMode) is (32, <= RiscvPagingMode.Sv32) or (64, not RiscvPagingMode.Sv32));

        return opts;
    }
}
