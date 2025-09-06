namespace SurrealNumber;

public static class SurrealNumberConverter
{
    public static string ConvertToString(this SurrealNum num) =>
        $$"""{{{num.L}}|{{num.R}}}""";

    public static string ConvertToFullString(this SurrealNum num)
    {
        var a = num.L.Select(x => x.ConvertToFullString());
        var b = num.R.Select(x => x.ConvertToFullString());
        return $$"""{{{string.Join(",", a)}}|{{string.Join(",", b)}}}""";
    }

    public static double ConvertToDouble(this SurrealNum num)
    {
        if (!num.L.Any() && !num.R.Any()) return 0;
        if (!num.L.Any()) return num.R.Min().ConvertToDouble() - 1;
        if (!num.R.Any()) return num.L.Max().ConvertToDouble() + 1;

        return (num.L.Sum(ConvertToDouble) + num.R.Sum(ConvertToDouble)) / 2;
    }

    public static T To<T>(this SurrealNum num)
    {
        if (typeof(T) == typeof(string)) return (T)(object)num.ToString();
        if (typeof(T) == typeof(double)) return (T)(object)num.ConvertToDouble();
        if (typeof(T) == typeof(float)) return (T)(object)(float)num.ConvertToDouble();
        if (typeof(T) == typeof(int)) return (T)(object)(int)num.ConvertToDouble().ToLong();
        if (typeof(T) == typeof(long)) return (T)(object)num.ConvertToDouble().ToLong();

        return Thrower.InvalidOpEx<T>();
    }

    private static long ToLong(this double value) => (long)(value + 0.1 * Math.Sign(value));
}