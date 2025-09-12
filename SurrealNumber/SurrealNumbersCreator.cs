using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealNumber;

public static class SurrealNumsCreator
{
    private const int SimplifyFastInternalLimit = 100;
    private static readonly Dictionary<SurrealNum, SurrealNum> _simplifiedCache = [];
    private static readonly Dictionary<int, List<SurrealNum>> _numbersCache = [];

    private static readonly List<SurrealNum> _positiveIntegersCache = [Zero];
    private static readonly List<SurrealNum> _negativeIntegersCache = [Zero];


    public static SurrealNum Simplify(this SurrealNum num)
    {
        if (!num.L.Any() && !num.R.Any()) return num;
        if (_simplifiedCache.TryGetValue(num, out var value)) return value;

        if (num.IsInteger()) return _simplifiedCache[num] = num;

        var real = num.ConvertToDouble();
        if (!double.IsFinite(real)) return _simplifiedCache[num] = num;
        if (real.IsInteger()) return _simplifiedCache[num] = GetSimpleInteger(real.ToLong());

        return _simplifiedCache[num] = SimplifyInternal(num, [MinusOne, Zero, One], 0);
    }

    private static SurrealNum SimplifyInternal(SurrealNum num, List<SurrealNum> prev, int depth)
    {
        if (depth >= SimplifyFastInternalLimit)
            Thrower.InvalidOpEx("Too much depth");

        var list = CreateNewBasedNumbers(prev);

        if (num < list[0])
        {
            var cur = list[0];
            while (num < cur) cur -= One;
            return SimplifyInternal(num, [cur, cur + One], depth + 1);
        }

        if (num > list[^1])
        {
            var cur = list[^1];
            while (num > cur) cur += One;
            return SimplifyInternal(num, [cur - One, cur], depth + 1);
        }

        for (var i = 0; i < list.Count; i++)
            if (list[i] == num)
                return list[i];
            else if (list[i] > num)
                return SimplifyInternal(num, [list[i - 1], list[i]], depth + 1);

        return Thrower.InvalidOpEx<SurrealNum>("Cannot find simplified number");
    }

    private static SurrealNum GetSimpleInteger(long value)
    {
        if (value >= 0)
            while (_positiveIntegersCache.Count <= value)
                _positiveIntegersCache.Add(_positiveIntegersCache[^1] + One);
        else
            while (_negativeIntegersCache.Count <= -value)
                _negativeIntegersCache.Add(_negativeIntegersCache[^1] - One);

        return value >= 0 ? _positiveIntegersCache[(int)value] : _negativeIntegersCache[(int)-value];
    }


    public static int GetBirthday(this SurrealNum num) =>
        num == Zero
            ? 0
            : int.Max(num.L.Any() ? num.L.Num().GetBirthday() : -1, num.R.Any() ? num.R.Num().GetBirthday() : -1) + 1;

    public static List<SurrealNum> GenerateNumbersForBirthday(int birthday)
    {
        Thrower.Assert(birthday <= 16, "Target birthday must be less than or equals to 16");

        if (_numbersCache.TryGetValue(birthday, out var list))
            return list;

        if (birthday == 0) return [Zero];

        var prev = GenerateNumbersForBirthday(birthday - 1);

        var res = CreateNewBasedNumbers(prev);

        return _numbersCache[birthday] = res;
    }

    private static List<SurrealNum> CreateNewBasedNumbers(List<SurrealNum> prev)
    {
        var res = (List<SurrealNum>) [];

        if (prev[0].IsInteger())
            res.Add(
                SurrealNum.CreateInternal(
                    new LeftSetGenerator(new SetListGenerator([])),
                    new RightSetGenerator(new SetListGenerator([prev[0]]))
                )
            );

        for (var i = 0; i < prev.Count - 1; i++)
        {
            var l = prev[i];
            var r = prev[i + 1];
            res.Add(l);
            res.Add(
                SurrealNum.CreateInternal(
                    new LeftSetGenerator(new SetListGenerator([l])),
                    new RightSetGenerator(new SetListGenerator([r]))
                )
            );
        }


        res.Add(prev[^1]);
        if (prev[^1].IsInteger())
            res.Add(
                SurrealNum.CreateInternal(
                    new LeftSetGenerator(new SetListGenerator([prev[^1]])),
                    new RightSetGenerator(new SetListGenerator([]))
                )
            );
        return res;
    }
}