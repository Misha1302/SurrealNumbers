using System.Diagnostics.CodeAnalysis;

namespace SurrealNumber;

public static class Thrower
{
    public static void Assert([DoesNotReturnIf(false)] bool cond)
    {
        if (!cond) throw new InvalidOperationException();
    }

    [DoesNotReturn] public static void InvalidOpEx() => throw new InvalidOperationException();

    [DoesNotReturn] public static T InvalidOpEx<T>() => throw new InvalidOperationException();
}