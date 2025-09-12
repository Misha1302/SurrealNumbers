namespace SurrealNumber;

public interface ISurrealNumberVerifier
{
    public bool IsCorrect(SurrealNum num);

    public bool IsIncreasing(IEnumerable<SurrealNum> e);

    public bool IsDecreasing(IEnumerable<SurrealNum> e);
}