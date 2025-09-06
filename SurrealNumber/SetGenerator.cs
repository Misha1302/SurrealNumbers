using System.Collections;

namespace SurrealNumber;

public class SetGenerator(ISetGenerator tryGetNext) : IEnumerable<SurrealNum>
{
    public IEnumerator<SurrealNum> GetEnumerator() => new SetGeneratorIterator(tryGetNext);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => string.Join(", ", this.Select(x => x.To<double>()));
}