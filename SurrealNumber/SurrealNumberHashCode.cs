namespace SurrealNumber;

public static class SurrealNumberHashCode
{
    public static int GetHash(SetGenerator l, SetGenerator r) =>
        HashCode.Combine(l, r);
}