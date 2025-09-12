namespace SurrealNumber;

public class SurrealNumberVerifierStub : ISurrealNumberVerifier
{
    public bool IsCorrect(SurrealNum num) => true;

    public bool IsIncreasing(IEnumerable<SurrealNum> e) => true;

    public bool IsDecreasing(IEnumerable<SurrealNum> e) => true;
}