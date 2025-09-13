namespace SurrealNumber;

public static class EnumerableOperations
{
    public static IEnumerable<T> Union<T>(params IEnumerable<T>[] enumerables)
    {
        return enumerables.Aggregate((IEnumerable<T>) [], (current, enumerable) => current.Union(enumerable));
    }
}