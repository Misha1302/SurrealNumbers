namespace SurrealNumber;

public static class SurrealNumberVerifier
{
    public static bool IsCorrect(this SurrealNum num) =>
        num.L.All(l => !num.R.Any(r => l >= r));
}