namespace Vezel.Niru.Translation;

internal static class TranslationExtensions
{
    public static string ToIRString(this TypeId type)
    {
        return type switch
        {
            TypeId.Int32 => "i32",
            TypeId.Int64 => "i64",
            TypeId.Float32 => "f32",
            TypeId.Float64 => "f64",
            TypeId.Vector => "vec",
            _ => throw new UnreachableException(),
        };
    }
}
