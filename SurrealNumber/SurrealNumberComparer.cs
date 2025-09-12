namespace SurrealNumber;

public static class SurrealNumberComparer
{
    private static readonly Dictionary<(SurrealNum, SurrealNum), bool> _ltCache = [];
    private static readonly Dictionary<(SetGenerator.SetEnumerable, SetGenerator.SetEnumerable), bool> _eqCache = [];

    public static bool Eq(SetGenerator.SetEnumerable a, SetGenerator.SetEnumerable b)
    {
        if (!a.Any() && !b.Any()) return true;
        if (a.Any() != b.Any()) return false;

        if (_eqCache.TryGetValue((a, b), out var value))
            return value;

        var x = a.Num(SurrealNumbersLimitations.NumDefaultCount);
        var y = b.Num(SurrealNumbersLimitations.NumDefaultCount);
        return x.ConvertToDouble().EqApprox(y.ConvertToDouble());
    }

    public static bool IsLessThanOrEquals(this SurrealNum x, SurrealNum y)
    {
        if (_ltCache.TryGetValue((x, y), out var result))
            return result;

        var a = !x.L.Any(xl => y <= xl);
        var b = !y.R.Any(yr => yr <= x);

        return _ltCache[(x, y)] = a && b;
    }
}