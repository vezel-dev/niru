namespace Vezel.Niru.Translation;

public sealed class TemporaryVariable : Variable
{
    public int Id { get; }

    internal TemporaryVariable(TranslationUnit unit, TypeId type, int id)
        : base(unit, type)
    {
        Id = id;
    }

    public void Kill()
    {
        Check.Operation((Writes.Count, Reads.Count) == (0, 0));

        Unit.RemoveTemporary(this);
    }

    public override string ToString()
    {
        return $"${Id}";
    }
}
