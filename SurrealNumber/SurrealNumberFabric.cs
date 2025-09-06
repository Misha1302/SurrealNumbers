namespace SurrealNumber;

public static class SurrealNumberFabric
{
    public static SurrealNum Create(SetGenerator l, SetGenerator r)
    {
        var num = SurrealNum.CreateInternal(l, r).To<double>();

        var surrealNum = SurrealNumberCache.Cache.TryGetValue(num, out var value)
            ? value
            : SurrealNumberCache.Cache[num] = SurrealNum.CreateInternal(l, r);

        return surrealNum;
    }

    public static SurrealNum New(SetGenerator l, SetGenerator r) => Create(l, r);
}