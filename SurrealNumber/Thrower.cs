using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SurrealNumber;

public static class Thrower
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Assert(
        [DoesNotReturnIf(false)] bool cond,
        [CallerArgumentExpression(nameof(cond))]
        string message = ""
    )
    {
        if (!cond)
            InvalidOpEx(message);
    }

    [MethodImpl(MethodImplOptions.NoInlining)] [DoesNotReturn]
    public static void InvalidOpEx(string message = "") => throw new InvalidOperationException(message);

    [MethodImpl(MethodImplOptions.NoInlining)] [DoesNotReturn]
    public static T InvalidOpEx<T>(string message = "") => throw new InvalidOperationException(message);

    [MethodImpl(MethodImplOptions.NoInlining)] [DoesNotReturn]
    public static void DivByZero(string message = "") => throw new DivideByZeroException(message);
}