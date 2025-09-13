using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealNumber;

public static class SurrealNumberBasicHelpOperations
{
    public static SurrealNum Half(this SurrealNum x) =>
        x * SurHalf;

    public static int Sign(this SurrealNum x) =>
        x < Zero ? -1 : x > Zero ? 1 : 0;
}