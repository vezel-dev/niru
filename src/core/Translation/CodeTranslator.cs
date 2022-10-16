namespace Vezel.Niru.Translation;

public abstract class CodeTranslator
{
    public MachineInfo Machine { get; }

    protected CodeTranslator(MachineInfo machine)
    {
        Check.Null(machine);

        Machine = machine;
    }
}
