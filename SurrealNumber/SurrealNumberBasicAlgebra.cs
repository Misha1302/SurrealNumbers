namespace SurrealNumber;

public static class SurrealNumberBasicAlgebra
{
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _addCache = [];
    private static readonly Dictionary<(SurrealNum, SurrealNum), bool> _ltCache = [];
    private static readonly Dictionary<SurrealNum, SurrealNum> _negateCache = [];

    public static bool IsLessThanOrEquals(this SurrealNum x, SurrealNum y)
    {
        if (_ltCache.TryGetValue((x, y), out var result))
            return result;

        var a = !x.L.Any(xl => y.IsLessThanOrEquals(xl));
        var b = !y.R.Any(yr => yr.IsLessThanOrEquals(x));
        return _ltCache[(x, y)] = a && b;
    }

    public static SurrealNum Add(this SurrealNum x, SurrealNum y)
    {
        if (_addCache.TryGetValue((x, y), out var result))
            return result;


        var leftSum = (IEnumerable<SurrealNum>) [];
        leftSum = x.L.Aggregate(leftSum, (current, xl) => current.Union([xl.Add(y)]));
        leftSum = y.L.Aggregate(leftSum, (current, yl) => current.Union([x.Add(yl)]));


        var rightSum = (IEnumerable<SurrealNum>) [];
        rightSum = x.R.Aggregate(rightSum, (current, xr) => current.Union([xr.Add(y)]));
        rightSum = y.R.Aggregate(rightSum, (current, yr) => current.Union([x.Add(yr)]));

        return _addCache[(x, y)] = SurrealNumberFabric.New(
            new SetGenerator(new EnumerableGenerator(leftSum)),
            new SetGenerator(new EnumerableGenerator(rightSum))
        );
    }

    public static SurrealNum Negate(this SurrealNum x)
    {
        if (_negateCache.TryGetValue(x, out var result))
            return result;

        var right = x.L.Select(a => -a);
        var left = x.R.Select(a => -a);

        return _negateCache[x] = SurrealNumberFabric.New(
            new SetGenerator(new EnumerableGenerator(left)),
            new SetGenerator(new EnumerableGenerator(right))
        );
    }
}