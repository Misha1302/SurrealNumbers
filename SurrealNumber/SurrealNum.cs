namespace SurrealNumber;

public class SurrealNum : IComparable<SurrealNum>
{
    private SurrealNum(SetGenerator l, SetGenerator r)
    {
        L = l;
        R = r;

        Thrower.Assert(this.IsCorrect());
    }

    public SetGenerator L { get; }
    public SetGenerator R { get; }

    public int CompareTo(SurrealNum? other)
    {
        Thrower.Assert(other is not null);
        return this <= other ? -1 : this == other ? 0 : 1;
    }

    public static SurrealNum CreateInternal(SetGenerator l, SetGenerator r) => new(l, r);

    public override string ToString() => this.ConvertToString();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return this == (SurrealNum)obj;
    }

    public override int GetHashCode() => SurrealNumberHashCode.GetHash(L, R);


    public static bool operator <=(SurrealNum a, SurrealNum b) => a.IsLessThanOrEquals(b);

    public static bool operator >=(SurrealNum a, SurrealNum b) => b <= a;

    public static bool operator ==(SurrealNum? a, SurrealNum? b)
    {
        if (a is null && b is null) return true;
        if (a is null) return false;
        if (b is null) return false;
        return a <= b && b <= a;
    }

    public static bool operator !=(SurrealNum a, SurrealNum b) => !(a == b);

    public static bool operator <(SurrealNum a, SurrealNum b) => !(a >= b);

    public static bool operator >(SurrealNum a, SurrealNum b) => !(a <= b);

    public static SurrealNum operator +(SurrealNum a, SurrealNum b) => a.Add(b);
}