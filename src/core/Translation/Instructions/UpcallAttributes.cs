namespace Vezel.Niru.Translation.Instructions;

[Flags]
public enum UpcallAttributes
{
    None = 0b000,
    HasSideEffects = 0b001,
    ReadsContext = 0b010,
    WritesContext = 0b100,
}
