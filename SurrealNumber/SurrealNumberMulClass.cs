using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealNumber;

public static class SurrealNumberMulClass
{
    private static readonly Dictionary<(SurrealNum, SurrealNum), SurrealNum> _mulCache = [];
    private static readonly Dictionary<SurrealNum, SurrealNum> _reciprocalCache = [];


    public static SurrealNum Mul(this SurrealNum x, SurrealNum y)
    {
        if (_mulCache.TryGetValue((x, y), out var result))
            return result;

        if (x.Sign() == -1) return _mulCache[(x, y)] = -Mul(-x, y);
        if (y.Sign() == -1) return _mulCache[(x, y)] = -Mul(x, -y);

        var left = EnumerableOperations.Union(
            x.L.SelectMany(_ => y.L, (xl, yl) => xl * y + x * yl - xl * yl),
            x.R.SelectMany(_ => y.R, (xr, yr) => xr * y + x * yr - xr * yr)
        ).Order();

        var right = EnumerableOperations.Union(
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

        var generator = new EnumerableGenerator(ReciprocalEnumerator(x, []));

        return _reciprocalCache[x] = SurrealNumberFabric.New(
            new LeftSetGenerator(generator),
            new RightSetGenerator(generator)
        );

        static IEnumerable<SurrealNum> ReciprocalEnumerator(SurrealNum x, List<SurrealNum> cache)
        {
            var guess = One;
            while (x * guess > One) guess = guess.Half();

            var old = Zero;

            var i = -1;
            while (true)
            {
                i++;

                if (i < cache.Count)
                {
                    guess = cache[i];
                }
                else
                {
                    guess *= Two - x * guess;
                    cache.Add(guess);
                }

                if (old == guess) break;
                yield return guess;
                old = guess;
            }
        }
    }
}