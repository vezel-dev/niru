namespace Vezel.Niru.Diagnostics;

[StackTraceHidden]
internal static class Check
{
    public static class Always
    {
        public static void Assert(
            [DoesNotReturnIf(false)] bool condition,
            [CallerArgumentExpression(nameof(condition))] string? expression = null)
        {
            if (!condition)
                throw new UnreachableException($"Hard assertion '{expression}' failed.");
        }
    }

    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void Assert(
            [DoesNotReturnIf(false)] bool condition,
            [CallerArgumentExpression(nameof(condition))] string? expression = null)
        {
            if (!condition)
                throw new UnreachableException($"Debug assertion '{expression}' failed.");
        }
    }

    public static class Release
    {
        [Conditional("RELEASE")]
        public static void Assert(
            [DoesNotReturnIf(false)] bool condition,
            [CallerArgumentExpression(nameof(condition))] string? expression = null)
        {
            if (!condition)
                throw new UnreachableException($"Release assertion '{expression}' failed.");
        }
    }

    public static void Argument<T>(
        [DoesNotReturnIf(false)] bool condition,
        in T value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        _ = value;

        if (!condition)
            throw new ArgumentException(null, name);
    }

    public static void Null([NotNull] object? value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        ArgumentNullException.ThrowIfNull(value, name);
    }

    public static unsafe void Null(void* value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        ArgumentNullException.ThrowIfNull(value, name);
    }

    public static void NullOrEmpty(
        [NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, name);
    }

    public static void Range<T>(
        [DoesNotReturnIf(false)] bool condition,
        in T value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        _ = value;

        if (!condition)
            throw new ArgumentOutOfRangeException(name);
    }

    public static void Range(Range value, int length)
    {
        _ = value.GetOffsetAndLength(length);
    }

    public static void Enum<T>(T value, [CallerArgumentExpression(nameof(value))] string? name = null)
        where T : struct, Enum
    {
        if (!System.Enum.IsDefined(value))
            throw new ArgumentOutOfRangeException(name);
    }

    public static void Enum<T>(T? value, [CallerArgumentExpression(nameof(value))] string? name = null)
        where T : struct, Enum
    {
        if (value is T v && !System.Enum.IsDefined(v))
            throw new ArgumentOutOfRangeException(name);
    }

    public static void Operation([DoesNotReturnIf(false)] bool condition)
    {
        if (!condition)
            throw new InvalidOperationException();
    }

    public static void All<T, TState>(
        IEnumerable<T> value,
        in TState state,
        Func<T, TState, bool> predicate,
        [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        foreach (var item in value)
            if (!predicate(item, state))
                throw new ArgumentException(null, name);
    }
}
