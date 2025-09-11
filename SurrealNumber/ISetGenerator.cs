namespace SurrealNumber;

public interface ISetGenerator
{
    public SurrealNum this[int index] { get; }
    public (bool, SurrealNum?) TryGetNext();
    public ISetGenerator Clone();
    int GetCount(int limit);
}