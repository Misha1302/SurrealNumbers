namespace SurrealNumber;

public static class SurrealNumberFabric
{
    private const int SafetySizeLimit = 100;

    public static SurrealNum Create(SetGenerator l, SetGenerator r)
    {
        var num = SurrealNum.CreateInternal(l, r).To<double>();

        SurrealNum surrealNum;
        if (l.GetCount(SafetySizeLimit) < SafetySizeLimit && r.GetCount(SafetySizeLimit) < SafetySizeLimit &&
            SurrealCacheNumbers.Cache.TryGetValue(num, out var value))
        {
            surrealNum = value;
        }
        else
        {
            (l, r) = Simplify(l, r);
            surrealNum = SurrealCacheNumbers.Cache[num] = SurrealNum.CreateInternal(l, r);
        }

        return surrealNum;
    }

    private static (SetGenerator l, SetGenerator r) Simplify(SetGenerator l, SetGenerator r)
    {
        var lcnt = l.GetCount(SafetySizeLimit);
        var rcnt = r.GetCount(SafetySizeLimit);

        if (lcnt >= SafetySizeLimit || rcnt >= SafetySizeLimit)
            return (l, r);

        return (
            new SetGenerator(new SetListGenerator(l.Any() ? [l.Max()] : [])),
            new SetGenerator(new SetListGenerator(r.Any() ? [r.Min()] : []))
        );
    }

    public static SurrealNum New(SetGenerator l, SetGenerator r) => Create(l, r);
}