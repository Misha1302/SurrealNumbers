using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SurrealNumber;

public static class Thrower
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert([DoesNotReturnIf(false)] bool cond)
    {
        if (!cond) InvalidOpEx();
    }

    [MethodImpl(MethodImplOptions.NoInlining)] [DoesNotReturn]
    public static void InvalidOpEx() => throw new InvalidOperationException();

    [MethodImpl(MethodImplOptions.NoInlining)] [DoesNotReturn]
    public static T InvalidOpEx<T>() => throw new InvalidOperationException();

    [MethodImpl(MethodImplOptions.NoInlining)] [DoesNotReturn]
    public static void DivByZero() => throw new DivideByZeroException();
}