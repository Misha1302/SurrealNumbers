using System.Collections;

namespace SurrealNumber;

public struct SetGeneratorIterator(ISetGenerator sourceGenerator) : IEnumerator<SurrealNum>
{
    private const int SafetySizeLimit = 1000;

    private int _movedCount;

    private ISetGenerator currentGenerator = sourceGenerator.Clone();
    private SurrealNum? _current = null;

    public bool MoveNext()
    {
        Thrower.Assert(++_movedCount <= SafetySizeLimit);

        (var success, _current) = currentGenerator.TryGetNext();
        return success;
    }

    public void Reset()
    {
        _movedCount = 0;
        currentGenerator = sourceGenerator.Clone();
    }

    public SurrealNum Current => _current ?? Thrower.InvalidOpEx<SurrealNum>();

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}