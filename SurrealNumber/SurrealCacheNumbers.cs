namespace SurrealNumber;

public static class SurrealCacheNumbers
{
    public static readonly Dictionary<(SetGenerator, SetGenerator), SurrealNum> Cache = new();

    public static readonly SurrealNum Zero = SurrealNum.CreateInternal(
        new LeftSetGenerator(new SetListGenerator([])),
        new RightSetGenerator(new SetListGenerator([]))
    );

    public static readonly SurrealNum One = SurrealNum.CreateInternal(
        new LeftSetGenerator(new SetListGenerator([Zero])),
        new RightSetGenerator(new SetListGenerator([]))
    );

    public static readonly SurrealNum MinusOne = SurrealNum.CreateInternal(
        new LeftSetGenerator(new SetListGenerator([])),
        new RightSetGenerator(new SetListGenerator([Zero]))
    );

    public static readonly SurrealNum SurHalf = SurrealNum.CreateInternal(
        new LeftSetGenerator(new SetListGenerator([Zero])),
        new RightSetGenerator(new SetListGenerator([One]))
    );

    public static readonly SurrealNum Two = One + One;
    public static readonly SurrealNum Three = Two + One;
    public static readonly SurrealNum Four = Two + Two;
    public static readonly SurrealNum Five = Four + One;
}