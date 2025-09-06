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
        var arr = (SurrealNum[]) [MinusOne, Zero, MinusOne + One, One, One + One];
        var res = arr.SelectMany(_ => arr, (a, b) => a + b).Select(x => x.To<double>());
        var ans = (List<int>) [-2, -1, -1, 0, 1, -1, 0, 0, 1, 2, -1, 0, 0, 1, 2, 0, 1, 1, 2, 3, 1, 2, 2, 3, 4];

        Assert.That(res, Is.EqualTo(ans));
    }
}