namespace SurrealNumber;

public static class NumbersExtensions
{
    public static bool EqApprox(this double a, double b, double accuracy = -1)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (accuracy < 0) accuracy = SurrealNumbersLimitations.MaxNumEqError;
        return Math.Abs(a - b) <= accuracy;
    }
}