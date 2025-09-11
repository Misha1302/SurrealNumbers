using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealArithmeticTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var arr = (SurrealNum[]) [MinusOne, Zero, One, One + One, MinusOne + One];
        var res = arr.SelectMany(_ => arr, (a, b) => a <= b);
        var ans = (List<bool>)
        [
            true, true, true, true, true, false, true, true, true, true, false, false, true, true, false, false, false,
            false, true, false, false, true, true, true, true,
        ];

        Assert.That(res, Is.EqualTo(ans));
    }

    [Test]
    public void Test2()
    {
        var arr = (SurrealNum[]) [MinusOne + MinusOne, MinusOne, Zero, MinusOne + One, One, One + One];
        var res = arr.Select(a => -a).Select(x => x.To<double>());
        var ans = (List<int>) [2, 1, 0, 0, -1, -2];

        Assert.That(res, Is.EqualTo(ans));
    }

    [Test]
    public void Test3()
    {
        var num = MinusOne + MinusOne + MinusOne;

        Assert.That(-num, Is.EqualTo(- - -num));
    }

    [Test]
    public void Test4()
    {
        var num = Two * Five + Three - (Two + Five);

        Assert.That(num.ConvertToDouble(), Is.EqualTo(6));
    }

    [Test]
    public void Test5()
    {
        var num = One + SurHalf;

        Assert.That(num.ConvertToDouble(), Is.EqualTo(1.5));
    }

    [Test]
    public void Test6()
    {
        var num = SurHalf * SurHalf * Two;

        Assert.That(num.ConvertToDouble(), Is.EqualTo(0.5));
    }

    [Test]
    public void Test7()
    {
        var num = One / Three;

        Assert.That(Math.Abs(num.ConvertToDouble() - 0.333333) < 0.01, Is.EqualTo(true));
    }

    [Test]
    public void Test8()
    {
        var num = (Five + Five + Five) * (SurHalf * SurHalf);

        Assert.That(num.ConvertToDouble(), Is.EqualTo(3.75));
    }

    [Test]
    public void Test9()
    {
        var num = (Five + Five + Five) * (SurHalf * SurHalf) - Five;

        Assert.That(num.ConvertToDouble(), Is.EqualTo(3.75 - 5));
    }

    [Test]
    public void Test10()
    {
        var num = Three * (Five / Three);

        Assert.That(Math.Abs(num.ConvertToDouble() - 5) < 0.01, Is.EqualTo(true));
    }
}