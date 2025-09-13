namespace SurrealNumber;

public struct EnumerableGenerator(IEnumerable<SurrealNum> enumerable) : ISetGenerator
{
    private readonly IEnumerator<SurrealNum> _enumerator = enumerable.GetEnumerator();
    private int _maxInsuredCount;

    public SurrealNum this[int index] => enumerable.ElementAt(int.Min(GetCount(index + 1) - 1, index));

    public (bool, SurrealNum?) TryGetNext()
    {
        var moveNext = _enumerator.MoveNext();
        return (moveNext, moveNext ? _enumerator.Current : null);
    }

    public ISetGenerator Clone() => new EnumerableGenerator(enumerable);

    public int GetCount(int limit)
    {
        if (limit <= _maxInsuredCount)
            return limit;

        var cnt = 0;

        foreach (var _ in enumerable)
        {
            cnt++;
            _maxInsuredCount = int.Max(_maxInsuredCount, cnt);
            if (cnt >= limit) break;
        }

        return cnt;
    }
}