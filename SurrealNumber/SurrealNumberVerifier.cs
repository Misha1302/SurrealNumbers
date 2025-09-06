namespace SurrealNumber;

public static class SurrealNumberVerifier
{
    public static bool IsCorrect(this SurrealNum num)
    {
        return num.L.All(l => num.R.All(r => r >= l));
    }
}