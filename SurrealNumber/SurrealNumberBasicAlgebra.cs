using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealNumber;

public static class SurrealNumberBasicAlgebra
{
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _mulCache = [];
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _addCache = [];
    private static readonly Dictionary<SurrealNum, SurrealNum> _reciprocalCache = [];
    private static readonly Dictionary<(SurrealNum, SurrealNum), bool> _ltCache = [];
    private static readonly Dictionary<SurrealNum, SurrealNum> _negateCache = [];

    public static bool IsLessThanOrEquals(this SurrealNum x, SurrealNum y)
    {
        if (_ltCache.TryGetValue((x, y), out var result))
            return result;

        var a = !x.L.Any(xl => y <= xl);
        var b = !y.R.Any(yr => yr <= x);

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

        var left = x.R.Select(a => -a);
        var right = x.L.Select(a => -a);

        return _negateCache[x] = SurrealNumberFabric.New(
            new SetGenerator(new EnumerableGenerator(left)),
            new SetGenerator(new EnumerableGenerator(right))
        );
    }


    public static SurrealNum Mul(this SurrealNum x, SurrealNum y)
    {
        if (_mulCache.TryGetValue((x, y), out var result))
            return result;

        var left = Union(
            x.L.SelectMany(_ => y.L, (xl, yl) => xl * y + x * yl - xl * yl),
            x.R.SelectMany(_ => y.R, (xr, yr) => xr * y + x * yr - xr * yr)
        );

        var right = Union(
            x.L.SelectMany(_ => y.R, (xl, yr) => xl * y + x * yr - xl * yr),
            x.R.SelectMany(_ => y.L, (xr, yl) => x * yl + xr * y - xr * yl)
        );

        return _mulCache[(x, y)] = SurrealNumberFabric.New(
            new SetGenerator(new EnumerableGenerator(left)),
            new SetGenerator(new EnumerableGenerator(right))
        );
    }

    public static SurrealNum Reciprocal(this SurrealNum x)
    {
        if (x == Zero) throw new DivideByZeroException("Cannot take reciprocal of zero.");
        if (x < Zero) return -Reciprocal(-x);

        if (_reciprocalCache.TryGetValue(x, out var result))
            return result;

        var leftSet = new HashSet<SurrealNum>();
        var rightSet = new HashSet<SurrealNum>();

        leftSet.Add(Zero);

        bool changed;
        var cnt = 0;
        do
        {
            changed = false;

            foreach (var xR in x.R.Where(xr => xr != Zero))
            {
                var invXr = Reciprocal(xR);
                foreach (var yL in leftSet.ToList())
                {
                    var term = (One + (xR - x) * yL) * invXr;
                    if (leftSet.Add(term)) changed = true;
                }
            }

            foreach (var xL in x.L.Where(xl => xl != Zero))
            {
                var invXl = Reciprocal(xL);
                foreach (var yR in rightSet.ToList())
                {
                    var term = (One + (xL - x) * yR) * invXl;
                    if (leftSet.Add(term)) changed = true;
                }
            }

            foreach (var xL in x.L.Where(xl => xl != Zero))
            {
                var invXl = Reciprocal(xL);
                foreach (var yL in leftSet.ToList())
                {
                    var term = (One + (xL - x) * yL) * invXl;
                    if (rightSet.Add(term)) changed = true;
                }
            }

            foreach (var xR in x.R.Where(xr => xr != Zero))
            {
                var invXr = Reciprocal(xR);
                foreach (var yR in rightSet.ToList())
                {
                    var term = (One + (xR - x) * yR) * invXr;
                    if (rightSet.Add(term)) changed = true;
                }
            }
        } while (changed && ++cnt < 15);

        var y = SurrealNumberFabric.New(
            new SetGenerator(new SetListGenerator(leftSet.ToList())),
            new SetGenerator(new SetListGenerator(rightSet.ToList()))
        );

        _reciprocalCache[x] = y;
        return y;
    }

    public static SurrealNum Middle(SurrealNum x, SurrealNum y)
    {
        if (x == y) return x;
        return (x + y) * SurHalf;
    }

    public static SurrealNum Half(this SurrealNum x) =>
        Middle(Min(x, Zero), Max(Zero, x));

    public static SurrealNum Max(this SurrealNum x, SurrealNum y) =>
        x >= y ? x : y;

    public static SurrealNum Min(this SurrealNum x, SurrealNum y) =>
        x <= y ? x : y;

    private static IEnumerable<T> Union<T>(params IEnumerable<T>[] enumerables)
    {
        return enumerables.Aggregate((IEnumerable<T>) [], (current, enumerable) => current.Union(enumerable));
    }
}