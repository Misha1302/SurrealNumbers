namespace SurrealNumber;

public class RightSetGenerator(ISetGenerator generator) : SetGenerator(generator)
{
    private readonly Dictionary<int, SurrealNum> _numCache = [];

    protected override SurrealNum NumInternal(int limit)
    {
        if (_numCache.TryGetValue(limit, out var num))
            return num;

        Thrower.Assert(TakeFirst(limit).IsDecreasing(), "Collection must be decreasing");
        return _numCache[limit] = Enumerable.Num(limit);
    }
}