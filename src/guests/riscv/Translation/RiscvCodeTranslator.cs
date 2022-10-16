namespace Vezel.Niru.Guests.Riscv.Translation;

public sealed class RiscvCodeTranslator : CodeTranslator
{
    public new RiscvMachineInfo Machine => Unsafe.As<RiscvMachineInfo>(base.Machine);

    public RiscvCodeTranslator(RiscvMachineInfo machine)
        : base(machine)
    {
    }
}
