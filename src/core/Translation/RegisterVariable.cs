namespace Vezel.Niru.Translation;

public sealed class RegisterVariable : Variable
{
    public RegisterInfo Register { get; }

    internal RegisterVariable(TranslationUnit unit, RegisterInfo register)
        : base(unit, register.Type)
    {
        Register = register;
    }

    public override string ToString()
    {
        return Register.ToString();
    }
}
