namespace SurrealNumber;

public static class SurrealNumberVerifier
{
    public static bool IsCorrect(this SurrealNum num)
    {
        if (!num.L.Any() && num.R.Any(x => !x.ConvertToDouble().IsInteger()))
            return false;
        if (!num.R.Any() && num.L.Any(x => !x.ConvertToDouble().IsInteger()))
            return false;
        return num.L.All(l => num.R.All(r => l < r));
    }
}