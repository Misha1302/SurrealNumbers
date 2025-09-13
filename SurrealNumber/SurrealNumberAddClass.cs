namespace SurrealNumber;

public static class SurrealNumberAddClass
{
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _addCache = [];
    private static readonly Dictionary<SurrealNum, SurrealNum> _negateCache = [];

    public static SurrealNum Add(this SurrealNum x, SurrealNum y)
    {
        if (_addCache.TryGetValue((x, y), out var result))
            return result;

        var leftSum = (IEnumerable<SurrealNum>) [];
        leftSum = x.L.Aggregate(leftSum, (current, num) => current.Union([num.Add(y)]));
        leftSum = y.L.Aggregate(leftSum, (current, yl) => current.Union([x.Add(yl)]));
        leftSum = leftSum.Order();

        var rightSum = (IEnumerable<SurrealNum>) [];
        rightSum = x.R.Aggregate(rightSum, (current, xr) => current.Union([xr.Add(y)]));
        rightSum = y.R.Aggregate(rightSum, (current, yr) => current.Union([x.Add(yr)]));
        rightSum = rightSum.OrderDescending();

        var leftSetGenerator = new LeftSetGenerator(new EnumerableGenerator(leftSum));
        var rightSetGenerator = new RightSetGenerator(new EnumerableGenerator(rightSum));
        var surrealNum = SurrealNumberFabric.New(
            leftSetGenerator,
            rightSetGenerator
        );

        return _addCache[(x, y)] = surrealNum;
    }

    public static SurrealNum Negate(this SurrealNum x)
    {
        if (_negateCache.TryGetValue(x, out var result))
            return result;

        var left = x.R.Select(a => -a);
        var right = x.L.Select(a => -a);

        return _negateCache[x] = SurrealNumberFabric.New(
            new LeftSetGenerator(new EnumerableGenerator(left)),
            new RightSetGenerator(new EnumerableGenerator(right))
        );
    }
}