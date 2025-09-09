namespace SurrealNumber;

public class RightSetGenerator(ISetGenerator generator) : SetGenerator(generator)
{
    public override SurrealNum Num(int limit = int.MaxValue) =>
        TakeFirst(limit).Min();
}