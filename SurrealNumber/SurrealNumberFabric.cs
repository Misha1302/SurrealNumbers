namespace SurrealNumber;

public static class SurrealNumberFabric
{
    private const int SafetySizeLimit = 100;

    public static SurrealNum Create(SetGenerator l, SetGenerator r)
    {
        var num = SurrealNum.CreateInternal(l, r).To<double>();

        var surrealNum =
            l.GetCount() <= SafetySizeLimit && r.GetCount() <= SafetySizeLimit &&
            SurrealCacheNumbers.Cache.TryGetValue(num, out var value)
                ? value
                : SurrealCacheNumbers.Cache[num] = SurrealNum.CreateInternal(l, r);

        return surrealNum;
    }

    public static SurrealNum New(SetGenerator l, SetGenerator r) => Create(l, r);
}