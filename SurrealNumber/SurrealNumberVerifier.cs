namespace SurrealNumber;

public static class SurrealNumberVerifier
{
    public static bool IsCorrect(this SurrealNum num)
    {
        return num.L.All(l => num.R.All(r => l < r));
    }

    public static bool IsIncreasing(this IEnumerable<SurrealNum> e)
    {
        var prev = (SurrealNum?)null;
        foreach (var num in e)
        {
            if (prev >= num)
                return false;
            prev = num;
        }

        return true;
    }

    public static bool IsDecreasing(this IEnumerable<SurrealNum> e)
    {
        var prev = (SurrealNum?)null;
        foreach (var num in e)
        {
            if (prev <= num)
                return false;
            prev = num;
        }

        return true;
    }
}