namespace SurrealNumber;

public class LeftSetGenerator(ISetGenerator generator) : SetGenerator(generator)
{
    private readonly Dictionary<int, SurrealNum> _numCache = [];

    protected override SurrealNum NumInternal(int limit)
    {
        if (_numCache.TryGetValue(limit, out var num))
            return num;

        Thrower.Assert(TakeFirst(limit).IsIncreasing(), "Collection must be increasing");
        return _numCache[limit] = Enumerable.Num(limit);
    }
}