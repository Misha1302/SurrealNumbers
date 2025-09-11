namespace SurrealNumber;

public static class SurrealNumberFabric
{
    public static SurrealNum Create(LeftSetGenerator l, RightSetGenerator r)
    {
        if (!l.Any() && !r.Any()) return SurrealCacheNumbers.Zero;

        SetGenerator l2 = l, r2 = r;
        if (l2.Equals(r2))
            return l.Num();

        var num = SurrealNum.CreateInternal(l, r).Simplify();

        return SurrealCacheNumbers.Cache.TryGetValue((l, r), out var value)
            ? value
            : SurrealCacheNumbers.Cache[(l, r)] = num;
    }

    public static SurrealNum New(LeftSetGenerator l, RightSetGenerator r) => Create(l, r);
}