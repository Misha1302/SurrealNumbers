using System.Collections;

namespace SurrealNumber;

public abstract class SetGenerator(ISetGenerator generator) : IEnumerable<SurrealNum>
{
    protected readonly SetEnumerable Enumerable = new(generator);
    private int _hashCode = -1;

    public IEnumerator<SurrealNum> GetEnumerator()
    {
        if (Enumerable.Any())
            yield return Num();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected IEnumerable<SurrealNum> TakeFirst(int size) =>
        Enumerable.Take(Math.Min(size, Enumerable.GetCount(size)));

    public int GetCount(int limit) =>
        Enumerable.GetCount(limit);


    public SurrealNum Num(int limit = -1) =>
        NumInternal(limit >= 0 ? limit : SurrealNumbersLimitations.NumDefaultCount);

    protected abstract SurrealNum NumInternal(int limit);

    public override string ToString() => string.Join(", ", this);

    public override int GetHashCode()
    {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        if (_hashCode != -1) return _hashCode;
        return _hashCode = Any() ? Num(SurrealNumbersLimitations.NumDefaultCount).GetHashCode() : 0;
    }

    public override bool Equals(object? obj) =>
        obj is SetGenerator other && Enumerable == other.Enumerable;


    public bool AllIntegers()
    {
        return this.All(a => a.IsInteger());
    }

    public bool Any() => GetCount(1) == 1;

    public class SetEnumerable(ISetGenerator generator) : IEnumerable<SurrealNum>
    {
        private readonly Dictionary<Guid, bool> _equalsCache = [];

        private readonly Guid _id = Guid.NewGuid();

        public IEnumerator<SurrealNum> GetEnumerator() =>
            new SetGeneratorIterator(generator);

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public override string ToString() =>
            string.Join(", ", this.Select(x => x.To<double>()));

        public int GetCount(int limit) =>
            generator.GetCount(limit);


        public SurrealNum Num(int limit) =>
            generator[int.Min(GetCount(limit) - 1, limit)];

        public static bool operator ==(SetEnumerable left, SetEnumerable right) =>
            left.Equals(right);

        public static bool operator !=(SetEnumerable left, SetEnumerable right) => !(left == right);

        public bool Equals(SetEnumerable other)
        {
            if (_equalsCache.TryGetValue(other._id, out var result))
                return result;

            return _equalsCache[other._id] = this.Eq(other);
        }

        public override bool Equals(object? obj) =>
            obj is SetEnumerable setGenerator && Equals(setGenerator);

        public override int GetHashCode() => this.Aggregate(0, HashCode.Combine);
    }
}