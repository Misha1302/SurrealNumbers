namespace SurrealNumber;

public static class SurrealNumberBasicAlgebra
{
    public static bool IsLessThanOrEquals(this SurrealNum x, SurrealNum y)
    {
        var a = !x.L.Any(y.IsLessThanOrEquals);
        var b = !y.R.Any(yr => yr.IsLessThanOrEquals(x));
        return a && b;
    }

    public static SurrealNum Add(this SurrealNum x, SurrealNum y)
    {
        var leftSum = (IEnumerable<SurrealNum>) [];
        leftSum = x.L.Aggregate(leftSum, (current, xl) => current.Union([xl.Add(y)]));
        leftSum = y.L.Aggregate(leftSum, (current, yl) => current.Union([x.Add(yl)]));


        var rightSum = (IEnumerable<SurrealNum>) [];
        rightSum = x.R.Aggregate(rightSum, (current, xr) => current.Union([xr.Add(y)]));
        rightSum = y.R.Aggregate(rightSum, (current, yr) => current.Union([x.Add(yr)]));

        return SurrealNumberFabric.New(
            new SetGenerator(new EnumerableGenerator(leftSum)),
            new SetGenerator(new EnumerableGenerator(rightSum))
        );
    }
}