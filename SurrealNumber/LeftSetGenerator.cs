namespace SurrealNumber;

public class LeftSetGenerator(ISetGenerator generator) : SetGenerator(generator)
{
    private readonly Dictionary<int, SurrealNum> _numCache = [];

    public override SurrealNum Num(int limit = int.MaxValue)
    {
        if (_numCache.TryGetValue(limit, out var num))
            return num;

        return _numCache[limit] = TakeFirst(limit).Max();
    }
}