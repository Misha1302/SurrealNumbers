namespace SurrealNumber;

public static class SurrealNumberVerifier
{
    public static ISurrealNumberVerifier Implementation = new SurrealNumberVerifierDefaultImplementation();

    public static bool IsCorrect(this SurrealNum num) => Implementation.IsCorrect(num);

    public static bool IsIncreasing(this IEnumerable<SurrealNum> e) => Implementation.IsIncreasing(e);

    public static bool IsDecreasing(this IEnumerable<SurrealNum> e) => Implementation.IsDecreasing(e);
}