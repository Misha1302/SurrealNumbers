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
    public void TestB0()
    {
        var num = Three * (Five / Three);

        Assert.That(Math.Abs(num.ConvertToDouble() - 5) < 0.01, Is.EqualTo(true));
    }

    [Test]
    public void TestB1()
    {
        var num = Five * Five * Five * Three;

        Assert.That(num.ConvertToDouble(), Is.EqualTo(5 * 5 * 5 * 3));
    }

    [Test]
    public void TestB2()
    {
        var num = Five * Five * Five * Three / Two;

        Assert.That(num.ConvertToDouble(), Is.EqualTo(5 * 5 * 5 * 3 / 2.0));
    }

    [Test]
    public void TestB3()
    {
        var lts = (bool[])
        [
            SurHalf * SurHalf * SurHalf < One,
            SurHalf * Two <= One,
            SurHalf * Two > SurHalf,
            Two * Three * SurHalf <= Three,
            Two * Three * SurHalf > Two + SurHalf,
        ];

        Assert.That(lts.All(x => x), Is.EqualTo(true));
    }

    [Test]
    public void TestB4()
    {
        var lts = (bool[])
        [
            One + One == Two,
            Five - Two == Three,
            Three - Five == -Two,
        ];

        Assert.That(lts.All(x => x), Is.EqualTo(true));
    }

    [Test]
    public void TestB5()
    {
        SurrealNumberVerifier.Implementation = new SurrealNumberVerifierStub();

        var num = Five * Five * Five * Three / Two;

        Assert.That(num.ConvertToDouble(), Is.EqualTo(5 * 5 * 5 * 3 / 2.0));
    }

    [Test]
    public void TestB6()
    {
        var num = Five * Five * Five * Three / Two;

        Assert.That(num.ConvertToFullString(), Is.EqualTo(
            "{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}|}}"
        ));
    }

    [Test]
    public void TestB7()
    {
        var nums = (List<SurrealNum>) [Five, SurHalf, SurHalf * SurHalf];

        Assert.That(nums.Select(x => x.GetBirthday()), Is.EqualTo((List<int>) [5, 2, 3]));
    }

    [Test]
    public void TestB8()
    {
        Assert.That((-Two * Five).ConvertToDouble(), Is.EqualTo(-10));
    }

    [Test]
    public void TestB9()
    {
        Assert.That((-Two * Five * -SurHalf).ConvertToDouble(), Is.EqualTo(-2 * 5 * -0.5));
    }

    [Test]
    public void TestC1()
    {
        Assert.That((-SurHalf / Three).ConvertToDouble().EqApprox(-0.5 / 3, 0.01), Is.EqualTo(true));
    }

    [Test]
    public void TestC2()
    {
        Assert.That((-(-SurHalf / Three)).ConvertToDouble().EqApprox(-(-0.5 / 3), 0.01), Is.EqualTo(true));
    }
}