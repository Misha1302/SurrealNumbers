namespace SurrealNumber;

public class SetListGenerator(List<SurrealNum> surreals) : ISetGenerator
{
    private int _index;

    public (bool, SurrealNum) TryGetNext() =>
        (_index < surreals.Count, _index < surreals.Count ? surreals[_index++] : default);

    public ISetGenerator Clone() => new SetListGenerator([..surreals]);

    public int GetCount() => surreals.Count;
}