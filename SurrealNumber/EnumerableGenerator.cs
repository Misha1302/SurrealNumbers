namespace SurrealNumber;

public readonly struct EnumerableGenerator(IEnumerable<SurrealNum> enumerable) : ISetGenerator
{
    private readonly IEnumerator<SurrealNum> _enumerator = enumerable.GetEnumerator();

    public (bool, SurrealNum) TryGetNext()
    {
        var moveNext = _enumerator.MoveNext();
        return (moveNext, moveNext ? _enumerator.Current : default);
    }

    public ISetGenerator Clone() => new EnumerableGenerator(enumerable);

    public int GetCount() => enumerable.Count();
}