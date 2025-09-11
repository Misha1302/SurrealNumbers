namespace SurrealNumber;

public class LeftSetGenerator(ISetGenerator generator) : SetGenerator(generator)
{
    private readonly Dictionary<int, SurrealNum> _numCache = [];

    public override SurrealNum Num(int limit = int.MaxValue)
    {
        if (_numCache.TryGetValue(limit, out var num))
          return num;

        Thrower.Assert(TakeFirst(limit).IsIncreasing(), "Collection must be increasing");
        return _numCache[limit] = Enumerable.Num(limit);
    }
}