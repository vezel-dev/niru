namespace Vezel.Niru;

public readonly ref struct ScopedAction<T1, T2, T3>
{
    private readonly Action<T1, T2, T3> _action;

    internal ScopedAction(Action<T1, T2, T3> action)
    {
        _action = action;
    }

    public void Invoke(T1 arg1, T2 arg2, T3 arg3)
    {
        Check.Operation(_action != null);

        _action.Invoke(arg1, arg2, arg3);
    }
}
