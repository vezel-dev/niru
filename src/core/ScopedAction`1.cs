namespace Vezel.Niru;

public readonly ref struct ScopedAction<T1>
{
    private readonly Action<T1> _action;

    internal ScopedAction(Action<T1> action)
    {
        _action = action;
    }

    public void Invoke(T1 arg1)
    {
        Check.Operation(_action != null);

        _action.Invoke(arg1);
    }
}
