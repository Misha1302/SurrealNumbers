using System.Diagnostics;
using static SurrealNumber.SurrealCacheNumbers;

var sw = Stopwatch.StartNew();

Console.WriteLine(One / Three);
Console.WriteLine(Three * (Five / Three));

Console.WriteLine(sw.ElapsedMilliseconds);