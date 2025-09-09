namespace SurrealNumber;

public static class SurrealNumberFabric
{
    private const int SafetySizeLimit = 100;

    public static SurrealNum Create(LeftSetGenerator l, RightSetGenerator r)
    {
        var num = SurrealNum.CreateInternal(l, r).To<double>();

        var surrealNum = l.GetCount(SafetySizeLimit) < SafetySizeLimit &&
                         r.GetCount(SafetySizeLimit) < SafetySizeLimit &&
                         SurrealCacheNumbers.Cache.TryGetValue(num, out var value)
            ? value
            : SurrealCacheNumbers.Cache[num] = SurrealNum.CreateInternal(l, r);

        return surrealNum;
    }

    public static SurrealNum New(LeftSetGenerator l, RightSetGenerator r) => Create(l, r);
}