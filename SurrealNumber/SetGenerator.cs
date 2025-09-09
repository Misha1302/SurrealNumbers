using System.Collections;

namespace SurrealNumber;

public abstract class SetGenerator(ISetGenerator generator) : IEnumerable<SurrealNum>
{
    private readonly SetEnumerable enumerable = new(generator);

    public IEnumerator<SurrealNum> GetEnumerator()
    {
        if (enumerable.Any())
            yield return Num();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected IEnumerable<SurrealNum> TakeFirst(int size) =>
        enumerable.Take(Math.Min(size, enumerable.GetCount(size)));

    public int GetCount(int limit = int.MaxValue) =>
        enumerable.GetCount(limit);


    public abstract SurrealNum Num(int limit = int.MaxValue);

    public override string ToString() => string.Join(", ", this);

    private class SetEnumerable(ISetGenerator generator) : IEnumerable<SurrealNum>
    {
        public IEnumerator<SurrealNum> GetEnumerator() =>
            new SetGeneratorIterator(generator);

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public override string ToString() =>
            string.Join(", ", this.Select(x => x.To<double>()));

        public int GetCount(int limit = int.MaxValue) =>
            generator.GetCount(limit);
    }
}