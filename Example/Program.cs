using System.Diagnostics;
using static SurrealNumber.SurrealCacheNumbers;

var sw = Stopwatch.StartNew();

var q = -SurHalf / Three;
Console.WriteLine(q);

Console.WriteLine(sw.ElapsedMilliseconds);