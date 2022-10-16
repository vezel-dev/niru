namespace Vezel.Niru.Guests.Riscv.Translation;

public sealed class RiscvMachineInfo : MachineInfo
{
    public RiscvOptions Options { get; }

    public override int WordSize => Options.RegisterLength;

    public override int VectorSize => Options.VectorLength;

    public RegisterInfo X0 => IntegerRegisters[0];

    public RegisterInfo X1 => IntegerRegisters[1];

    public RegisterInfo X2 => IntegerRegisters[2];

    public RegisterInfo X3 => IntegerRegisters[3];

    public RegisterInfo X4 => IntegerRegisters[4];

    public RegisterInfo X5 => IntegerRegisters[5];

    public RegisterInfo X6 => IntegerRegisters[6];

    public RegisterInfo X7 => IntegerRegisters[7];

    public RegisterInfo X8 => IntegerRegisters[8];

    public RegisterInfo X9 => IntegerRegisters[9];

    public RegisterInfo X10 => IntegerRegisters[10];

    public RegisterInfo X11 => IntegerRegisters[11];

    public RegisterInfo X12 => IntegerRegisters[12];

    public RegisterInfo X13 => IntegerRegisters[13];

    public RegisterInfo X14 => IntegerRegisters[14];

    public RegisterInfo X15 => IntegerRegisters[15];

    public RegisterInfo X16 => GetOptionalIntegerRegister(16);

    public RegisterInfo X17 => GetOptionalIntegerRegister(17);

    public RegisterInfo X18 => GetOptionalIntegerRegister(18);

    public RegisterInfo X19 => GetOptionalIntegerRegister(19);

    public RegisterInfo X20 => GetOptionalIntegerRegister(20);

    public RegisterInfo X21 => GetOptionalIntegerRegister(21);

    public RegisterInfo X22 => GetOptionalIntegerRegister(22);

    public RegisterInfo X23 => GetOptionalIntegerRegister(23);

    public RegisterInfo X24 => GetOptionalIntegerRegister(24);

    public RegisterInfo X25 => GetOptionalIntegerRegister(25);

    public RegisterInfo X26 => GetOptionalIntegerRegister(26);

    public RegisterInfo X27 => GetOptionalIntegerRegister(27);

    public RegisterInfo X28 => GetOptionalIntegerRegister(28);

    public RegisterInfo X29 => GetOptionalIntegerRegister(29);

    public RegisterInfo X30 => GetOptionalIntegerRegister(30);

    public RegisterInfo X31 => GetOptionalIntegerRegister(31);

    public RegisterInfo F0 => GetFloatRegister(0);

    public RegisterInfo F1 => GetFloatRegister(1);

    public RegisterInfo F2 => GetFloatRegister(2);

    public RegisterInfo F3 => GetFloatRegister(3);

    public RegisterInfo F4 => GetFloatRegister(4);

    public RegisterInfo F5 => GetFloatRegister(5);

    public RegisterInfo F6 => GetFloatRegister(6);

    public RegisterInfo F7 => GetFloatRegister(7);

    public RegisterInfo F8 => GetFloatRegister(8);

    public RegisterInfo F9 => GetFloatRegister(9);

    public RegisterInfo F10 => GetFloatRegister(10);

    public RegisterInfo F11 => GetFloatRegister(11);

    public RegisterInfo F12 => GetFloatRegister(12);

    public RegisterInfo F13 => GetFloatRegister(13);

    public RegisterInfo F14 => GetFloatRegister(14);

    public RegisterInfo F15 => GetFloatRegister(15);

    public RegisterInfo F16 => GetFloatRegister(16);

    public RegisterInfo F17 => GetFloatRegister(17);

    public RegisterInfo F18 => GetFloatRegister(18);

    public RegisterInfo F19 => GetFloatRegister(19);

    public RegisterInfo F20 => GetFloatRegister(20);

    public RegisterInfo F21 => GetFloatRegister(21);

    public RegisterInfo F22 => GetFloatRegister(22);

    public RegisterInfo F23 => GetFloatRegister(23);

    public RegisterInfo F24 => GetFloatRegister(24);

    public RegisterInfo F25 => GetFloatRegister(25);

    public RegisterInfo F26 => GetFloatRegister(26);

    public RegisterInfo F27 => GetFloatRegister(27);

    public RegisterInfo F28 => GetFloatRegister(28);

    public RegisterInfo F29 => GetFloatRegister(29);

    public RegisterInfo F30 => GetFloatRegister(30);

    public RegisterInfo F31 => GetFloatRegister(31);

    public RegisterInfo V0 => GetVectorRegister(0);

    public RegisterInfo V1 => GetVectorRegister(1);

    public RegisterInfo V2 => GetVectorRegister(2);

    public RegisterInfo V3 => GetVectorRegister(3);

    public RegisterInfo V4 => GetVectorRegister(4);

    public RegisterInfo V5 => GetVectorRegister(5);

    public RegisterInfo V6 => GetVectorRegister(6);

    public RegisterInfo V7 => GetVectorRegister(7);

    public RegisterInfo V8 => GetVectorRegister(8);

    public RegisterInfo V9 => GetVectorRegister(9);

    public RegisterInfo V10 => GetVectorRegister(10);

    public RegisterInfo V11 => GetVectorRegister(11);

    public RegisterInfo V12 => GetVectorRegister(12);

    public RegisterInfo V13 => GetVectorRegister(13);

    public RegisterInfo V14 => GetVectorRegister(14);

    public RegisterInfo V15 => GetVectorRegister(15);

    public RegisterInfo V16 => GetVectorRegister(16);

    public RegisterInfo V17 => GetVectorRegister(17);

    public RegisterInfo V18 => GetVectorRegister(18);

    public RegisterInfo V19 => GetVectorRegister(19);

    public RegisterInfo V20 => GetVectorRegister(20);

    public RegisterInfo V21 => GetVectorRegister(21);

    public RegisterInfo V22 => GetVectorRegister(22);

    public RegisterInfo V23 => GetVectorRegister(23);

    public RegisterInfo V24 => GetVectorRegister(24);

    public RegisterInfo V25 => GetVectorRegister(25);

    public RegisterInfo V26 => GetVectorRegister(26);

    public RegisterInfo V27 => GetVectorRegister(27);

    public RegisterInfo V28 => GetVectorRegister(28);

    public RegisterInfo V29 => GetVectorRegister(29);

    public RegisterInfo V30 => GetVectorRegister(30);

    public RegisterInfo V31 => GetVectorRegister(31);

    public RiscvMachineInfo(RiscvOptions options)
    {
        Check.Null(options);

        Options = options.Normalize();
    }

    private RegisterInfo GetOptionalIntegerRegister(int index)
    {
        Check.Operation(!Options.ExtensionE);

        return IntegerRegisters[index];
    }

    private RegisterInfo GetFloatRegister(int index)
    {
        Check.Operation(Options.ExtensionF && !Options.ExtensionZfinx);

        return FloatRegisters[index];
    }

    private RegisterInfo GetVectorRegister(int index)
    {
        Check.Operation(Options.ExtensionV);

        return VectorRegisters[index];
    }

    protected override void CreateSystemRegisters(ScopedAction<int, TypeId, string> creator)
    {
        throw new NotImplementedException(); // TODO
    }

    protected override void CreateIntegerRegisters(ScopedAction<string> creator)
    {
        creator.Invoke("zero");
        creator.Invoke("ra");
        creator.Invoke("sp");
        creator.Invoke("gp");
        creator.Invoke("tp");
        creator.Invoke("t0");
        creator.Invoke("t1");
        creator.Invoke("t2");
        creator.Invoke("fp");
        creator.Invoke("s1");
        creator.Invoke("a0");
        creator.Invoke("a1");
        creator.Invoke("a2");
        creator.Invoke("a3");
        creator.Invoke("a4");
        creator.Invoke("a5");

        if (Options.ExtensionE)
            return;

        creator.Invoke("a6");
        creator.Invoke("a7");
        creator.Invoke("s2");
        creator.Invoke("s3");
        creator.Invoke("s4");
        creator.Invoke("s5");
        creator.Invoke("s6");
        creator.Invoke("s7");
        creator.Invoke("s8");
        creator.Invoke("s9");
        creator.Invoke("s10");
        creator.Invoke("s11");
        creator.Invoke("t3");
        creator.Invoke("t4");
        creator.Invoke("t5");
        creator.Invoke("t6");
    }

    protected override void CreateFloatRegisters(ScopedAction<TypeId, string> creator)
    {
        if (!Options.ExtensionF || Options.ExtensionZfinx)
            return;

        var type = Options.ExtensionD ? TypeId.Float64 : TypeId.Float32;

        creator.Invoke(type, "ft0");
        creator.Invoke(type, "ft1");
        creator.Invoke(type, "ft2");
        creator.Invoke(type, "ft3");
        creator.Invoke(type, "ft4");
        creator.Invoke(type, "ft5");
        creator.Invoke(type, "ft6");
        creator.Invoke(type, "ft7");
        creator.Invoke(type, "fs0");
        creator.Invoke(type, "fs1");
        creator.Invoke(type, "fa0");
        creator.Invoke(type, "fa1");
        creator.Invoke(type, "fa2");
        creator.Invoke(type, "fa3");
        creator.Invoke(type, "fa4");
        creator.Invoke(type, "fa5");
        creator.Invoke(type, "fa6");
        creator.Invoke(type, "fa7");
        creator.Invoke(type, "fs2");
        creator.Invoke(type, "fs3");
        creator.Invoke(type, "fs4");
        creator.Invoke(type, "fs5");
        creator.Invoke(type, "fs6");
        creator.Invoke(type, "fs7");
        creator.Invoke(type, "fs8");
        creator.Invoke(type, "fs9");
        creator.Invoke(type, "fs10");
        creator.Invoke(type, "fs11");
        creator.Invoke(type, "ft8");
        creator.Invoke(type, "ft9");
        creator.Invoke(type, "ft10");
        creator.Invoke(type, "ft11");
    }

    protected override void CreateVectorRegisters(ScopedAction<string> creator)
    {
        if (!Options.ExtensionV)
            return;

        creator.Invoke("vmask");
        creator.Invoke("vt0");
        creator.Invoke("vt1");
        creator.Invoke("vt2");
        creator.Invoke("vt3");
        creator.Invoke("vt4");
        creator.Invoke("vt5");
        creator.Invoke("vt6");
        creator.Invoke("va0");
        creator.Invoke("va1");
        creator.Invoke("va2");
        creator.Invoke("va3");
        creator.Invoke("va4");
        creator.Invoke("va5");
        creator.Invoke("va6");
        creator.Invoke("va7");
        creator.Invoke("va8");
        creator.Invoke("va9");
        creator.Invoke("va10");
        creator.Invoke("va11");
        creator.Invoke("va12");
        creator.Invoke("va13");
        creator.Invoke("va14");
        creator.Invoke("va15");
        creator.Invoke("vt7");
        creator.Invoke("vt8");
        creator.Invoke("vt9");
        creator.Invoke("vt10");
        creator.Invoke("vt11");
        creator.Invoke("vt12");
        creator.Invoke("vt13");
        creator.Invoke("vt14");
    }
}
