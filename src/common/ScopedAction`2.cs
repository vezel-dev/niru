using Vezel.Niru.Diagnostics;

namespace Vezel.Niru;

public ref struct ScopedAction<T1, T2>
{
    private readonly Action<T1, T2> _action;

    internal ScopedAction(Action<T1, T2> action)
    {
        _action = action;
    }

    public void Invoke(T1 arg1, T2 arg2)
    {
        Check.Operation(_action != null);

        _action.Invoke(arg1, arg2);
    }
}
