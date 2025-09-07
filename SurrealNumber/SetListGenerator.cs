namespace SurrealNumber;

public struct SetListGenerator(IList<SurrealNum> surreals) : ISetGenerator
{
    private int _index;

    public (bool, SurrealNum) TryGetNext() =>
        (_index < surreals.Count, _index < surreals.Count ? surreals[_index++] : default);

    public ISetGenerator Clone() => new SetListGenerator([..surreals]);

    public int GetCount(int limit) => Math.Min(limit, surreals.Count);
}