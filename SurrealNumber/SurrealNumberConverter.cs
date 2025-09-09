namespace SurrealNumber;

public static class SurrealNumberConverter
{
    private static readonly Dictionary<SurrealNum, double> _doubleCache = [];

    public static string ConvertToString(this SurrealNum num) =>
        $$"""{{{num.L}}|{{num.R}}}""";

    public static string ConvertToFullString(this SurrealNum num)
    {
        var a = num.L.Any() ? num.L.Num().ConvertToFullString() : "";
        var b = num.R.Any() ? num.R.Min().ConvertToFullString() : "";
        return $$"""{{{string.Join(",", a)}}|{{string.Join(",", b)}}}""";
    }

    public static double ConvertToDouble(this SurrealNum num)
    {
        if (_doubleCache.TryGetValue(num, out var result))
            return result;

        if (!num.L.Any() && !num.R.Any()) return 0;

        if (!num.L.Any())
            return _doubleCache[num] = num.R.Num().ConvertToDouble() - (num.R.AllIntegers() ? 1 : 0);
        if (!num.R.Any())
            return _doubleCache[num] = num.L.Num().ConvertToDouble() + (num.L.AllIntegers() ? 1 : 0);

        return _doubleCache[num] = (num.L.Num().ConvertToDouble() + num.R.Num().ConvertToDouble()) / 2;
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