namespace SurrealNumber;

public static class SurrealCacheNumbers
{
    public static readonly Dictionary<double, SurrealNum> Cache = new();

    public static readonly SurrealNum Zero = SurrealNumberFabric.New(
        new SetGenerator(new SetListGenerator([])),
        new SetGenerator(new SetListGenerator([]))
    );

    public static readonly SurrealNum One = SurrealNumberFabric.New(
        new SetGenerator(new SetListGenerator([Zero])),
        new SetGenerator(new SetListGenerator([]))
    );

    public static readonly SurrealNum MinusOne = SurrealNumberFabric.New(
        new SetGenerator(new SetListGenerator([])),
        new SetGenerator(new SetListGenerator([Zero]))
    );

    public static readonly SurrealNum SurHalf = SurrealNumberFabric.New(
        new SetGenerator(new SetListGenerator([Zero])),
        new SetGenerator(new SetListGenerator([One]))
    );

    public static readonly SurrealNum Two = One + One;

    public static readonly SurrealNum Three = Two + One;
}