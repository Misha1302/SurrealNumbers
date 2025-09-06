using System.Collections;

namespace SurrealNumber;

public class SetGeneratorIterator(ISetGenerator sourceGenerator) : IEnumerator<SurrealNum>
{
    private ISetGenerator currentGenerator = sourceGenerator.Clone();

    public bool MoveNext()
    {
        (var success, Current) = currentGenerator.TryGetNext();
        return success;
    }

    public void Reset()
    {
        currentGenerator = sourceGenerator.Clone();
    }

    public SurrealNum Current { get; private set; } = null!;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}