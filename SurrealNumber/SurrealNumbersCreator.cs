using System.Runtime.InteropServices;
using static SurrealNumber.SurrealCacheNumbers;

namespace SurrealNumber;

public static class SurrealNumsCreator
{
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
        if (!double.IsFinite(real)) return num;
        if (real.IsInteger()) return GetSimpleInteger(real.ToLong());

        // TODO: remade to recursive O(log2) 
        var top = int.Min(16, num.GetBirthday() + 1);
        for (var birthday = 0; birthday <= top; birthday++)
        {
            var span = BinSearchViaBirthday(num, birthday, out var ind);
            if (ind >= 0) return _simplifiedCache[num] = span[ind];
        }

        var span2 = BinSearchViaBirthday(num, top, out var ind2);
        ind2 = ~ind2;
        if (ind2 >= 1)
            return _simplifiedCache[num] = span2[ind2 - 1];

        return Thrower.InvalidOpEx<SurrealNum>($"Cannot find simple form for {num.ToString()}");
    }

    private static Span<SurrealNum> BinSearchViaBirthday(SurrealNum num, int birthday, out int ind)
    {
        var nums = GenerateNumbersForBirthday(birthday);
        var span = CollectionsMarshal.AsSpan(nums);
        ind = span.BinarySearch(num);
        return span;
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

        var res = (List<SurrealNum>) [];

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
        res.Add(
            SurrealNum.CreateInternal(
                new LeftSetGenerator(new SetListGenerator([prev[^1]])),
                new RightSetGenerator(new SetListGenerator([]))
            )
        );

        return _numbersCache[birthday] = res;
    }
}