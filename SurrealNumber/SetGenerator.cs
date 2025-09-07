using System.Collections;

namespace SurrealNumber;

public class SetGenerator(ISetGenerator generator) : IEnumerable<SurrealNum>
{
    public IEnumerator<SurrealNum> GetEnumerator() => new SetGeneratorIterator(generator);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => string.Join(", ", this.Select(x => x.To<double>()));

    public int GetCount(int limit = int.MaxValue) => generator.GetCount(limit);
}