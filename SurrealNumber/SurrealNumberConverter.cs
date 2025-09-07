namespace SurrealNumber;

public static class SurrealNumberConverter
{
    private static readonly Dictionary<SurrealNum, double> _doubleCache = [];

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
        if (_doubleCache.TryGetValue(num, out var result))
            return result;

        if (!num.L.Any() && !num.R.Any()) return 0;
        if (!num.L.Any()) return _doubleCache[num] = num.R.Min().ConvertToDouble() - 1;
        if (!num.R.Any()) return _doubleCache[num] = num.L.Max().ConvertToDouble() + 1;

        return _doubleCache[num] = (num.L.Max(ConvertToDouble) + num.R.Min(ConvertToDouble)) / 2;
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

    public static long ToLong(this double value) => (long)(value + 0.1 * Math.Sign(value));
    public static bool IsInteger(this double value) => Math.Abs(value - value.ToLong()) < 0.00001;
}