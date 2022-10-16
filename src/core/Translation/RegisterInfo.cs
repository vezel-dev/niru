namespace Vezel.Niru.Translation;

public sealed class RegisterInfo
{
    public MachineInfo Machine { get; }

    public RegisterBank Bank { get; }

    public int Id { get; }

    public TypeId Type { get; }

    public string Name { get; }

    internal RegisterInfo(MachineInfo machine, RegisterBank bank, int id, TypeId type, string name)
    {
        Machine = machine;
        Bank = bank;
        Id = id;
        Type = type;
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}
