using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealNumber;

public static class SurrealNumberBasicAlgebra
{
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _mulCache = [];
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _addCache = [];
    private static readonly Dictionary<(SurrealNum, SurrealNum), bool> _ltCache = [];
    private static readonly Dictionary<SurrealNum, SurrealNum> _reciprocalCache = [];
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
        leftSum = x.L.Aggregate(leftSum, (current, num) => current.Union([num.Add(y)]));
        leftSum = y.L.Aggregate(leftSum, (current, yl) => current.Union([x.Add(yl)]));
        leftSum = leftSum.Order();

        var rightSum = (IEnumerable<SurrealNum>) [];
        rightSum = x.R.Aggregate(rightSum, (current, xr) => current.Union([xr.Add(y)]));
        rightSum = y.R.Aggregate(rightSum, (current, yr) => current.Union([x.Add(yr)]));
        rightSum = rightSum.OrderDescending();

        var leftSetGenerator = new LeftSetGenerator(new EnumerableGenerator(leftSum));
        var rightSetGenerator = new RightSetGenerator(new EnumerableGenerator(rightSum));
        return _addCache[(x, y)] = SurrealNumberFabric.New(
            leftSetGenerator,
            rightSetGenerator
        );
    }

    public static SurrealNum Negate(this SurrealNum x)
    {
        if (_negateCache.TryGetValue(x, out var result))
            return result;

        var left = x.R.Select(a => -a).Order();
        var right = x.L.Select(a => -a).OrderDescending();

        return _negateCache[x] = SurrealNumberFabric.New(
            new LeftSetGenerator(new EnumerableGenerator(left)),
            new RightSetGenerator(new EnumerableGenerator(right))
        );
    }


    public static SurrealNum Mul(this SurrealNum x, SurrealNum y)
    {
        if (x >= y) (x, y) = (y, x);

        if (_mulCache.TryGetValue((x, y), out var result))
            return result;

        if (x.Sign() == -1) return _mulCache[(x, y)] = -Mul(-x, y);
        if (y.Sign() == -1) return _mulCache[(x, y)] = -Mul(x, -y);

        var left = Union(
            x.L.SelectMany(_ => y.L, (xl, yl) => xl * y + x * yl - xl * yl),
            x.R.SelectMany(_ => y.R, (xr, yr) => xr * y + x * yr - xr * yr)
        ).Order();

        var right = Union(
            x.L.SelectMany(_ => y.R, (xl, yr) => xl * y + x * yr - xl * yr),
            x.R.SelectMany(_ => y.L, (xr, yl) => x * yl + xr * y - xr * yl)
        ).OrderDescending();

        return _mulCache[(x, y)] = SurrealNumberFabric.New(
            new LeftSetGenerator(new EnumerableGenerator(left)),
            new RightSetGenerator(new EnumerableGenerator(right))
        );
    }

    public static SurrealNum Reciprocal(this SurrealNum x)
    {
        if (_reciprocalCache.TryGetValue(x, out var result))
            return result;

        var generator = new EnumerableGenerator(ReciprocalEnumerator(x));

        return _reciprocalCache[x] = SurrealNumberFabric.New(
            new LeftSetGenerator(generator),
            new RightSetGenerator(generator)
        );

        static IEnumerable<SurrealNum> ReciprocalEnumerator(SurrealNum x)
        {
            var guess = One;
            while (x * guess > One) guess = guess.Half();

            var old = Zero;
            // TODO: add support of infinity numbers
            var i = 0;
            while (i++ < 2)
            {
                guess *= Two - x * guess;

                if (old == guess) break;

                yield return guess;

                old = guess;
            }
        }
    }

    public static SurrealNum Middle(SurrealNum x, SurrealNum y)
    {
        if (x == y) return x;
        return (x + y) * SurHalf;
    }

    public static SurrealNum Half(this SurrealNum x) =>
        Middle(Zero, x);

    public static SurrealNum Max(this SurrealNum x, SurrealNum y) =>
        x >= y ? x : y;

    public static SurrealNum Min(this SurrealNum x, SurrealNum y) =>
        x <= y ? x : y;

    public static int Sign(this SurrealNum x) =>
        x < Zero ? -1 : x > Zero ? 1 : 0;

    private static IEnumerable<T> Union<T>(params IEnumerable<T>[] enumerables)
    {
        return enumerables.Aggregate((IEnumerable<T>) [], (current, enumerable) => current.Union(enumerable));
    }
}