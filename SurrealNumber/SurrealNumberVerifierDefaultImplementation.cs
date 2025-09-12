namespace SurrealNumber;

public class SurrealNumberVerifierDefaultImplementation : ISurrealNumberVerifier
{
    public bool IsCorrect(SurrealNum num)
    {
        return num.L.All(l => num.R.All(r => l < r));
    }

    public bool IsIncreasing(IEnumerable<SurrealNum> e)
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

    public bool IsDecreasing(IEnumerable<SurrealNum> e)
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