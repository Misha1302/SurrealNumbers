namespace SurrealNumber;

public interface ISetGenerator
{
    public (bool, SurrealNum) TryGetNext();
    public ISetGenerator Clone();
    int Count();
}