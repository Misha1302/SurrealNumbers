namespace SurrealNumber;

public static class SurrealNumberFabric
{
    private const int SafetySizeLimit = 100;

    public static SurrealNum Create(SetGenerator l, SetGenerator r)
    {
        var num = SurrealNum.CreateInternal(l, r).To<double>();

        var surrealNum =
            l.Count() <= SafetySizeLimit && r.Count() <= SafetySizeLimit &&
            SurrealNumberCache.Cache.TryGetValue(num, out var value)
                ? value
                : SurrealNumberCache.Cache[num] = SurrealNum.CreateInternal(l, r);

        return surrealNum;
    }

    public static SurrealNum New(SetGenerator l, SetGenerator r) => Create(l, r);
}