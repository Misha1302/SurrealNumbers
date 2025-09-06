using System.Diagnostics;
using SurrealNumber;

var minOne = SurrealCacheNumbers.MinusOne;
var zero = SurrealCacheNumbers.Zero;
var one = SurrealCacheNumbers.One;

var arr = (Span<SurrealNum>) [minOne, zero, one];
//
// foreach (var a in arr)
// foreach (var b in arr)
//     Console.WriteLine($"{a.To<double>()} {b.To<double>()} {(a + b).To<double>()} {(a + b).To<string>()}");

var sw = Stopwatch.StartNew();
var big = zero;
for (var i = 1; i <= 300; i++) big += one;
Console.WriteLine(sw.ElapsedMilliseconds + "ms");


sw = Stopwatch.StartNew();
big += big;
Console.WriteLine(sw.ElapsedMilliseconds + "ms");

Console.WriteLine(big.ConvertToDouble());
