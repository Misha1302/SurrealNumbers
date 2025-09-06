using System.Globalization;

namespace SurrealNumber;

public readonly struct SurrealNum : IComparable<SurrealNum>, IEquatable<SurrealNum>
{
    private SurrealNum(SetGenerator l, SetGenerator r)
    {
        L = l;
        R = r;

        Thrower.Assert(this.IsCorrect());
    }

    public readonly SetGenerator L;
    public readonly SetGenerator R;


    public static SurrealNum CreateInternal(SetGenerator l, SetGenerator r) => new(l, r);

    public override string ToString() => this.ConvertToDouble().ToString(CultureInfo.InvariantCulture);


    public override int GetHashCode() => SurrealNumberHashCode.GetHash(L, R);


    public static bool operator <=(SurrealNum a, SurrealNum b) => a.IsLessThanOrEquals(b);

    public static bool operator >=(SurrealNum a, SurrealNum b) => b <= a;

    public static bool operator ==(SurrealNum a, SurrealNum b) => a <= b && b <= a;

    public static bool operator !=(SurrealNum a, SurrealNum b) => !(a == b);

    public static bool operator <(SurrealNum a, SurrealNum b) => !(a >= b);

    public static bool operator >(SurrealNum a, SurrealNum b) => !(a <= b);

    public static SurrealNum operator *(SurrealNum a, SurrealNum b) => a.Mul(b);
    public static SurrealNum operator +(SurrealNum a, SurrealNum b) => a.Add(b);
    public static SurrealNum operator -(SurrealNum a, SurrealNum b) => a + -b;
    public static SurrealNum operator -(SurrealNum a) => a.Negate();

    public bool Equals(SurrealNum other) => L.Equals(other.L) && R.Equals(other.R);

    public override bool Equals(object? obj) => obj is SurrealNum other && Equals(other);

    public int CompareTo(SurrealNum other) => this <= other ? -1 : this == other ? 0 : 1;
}