using System.Diagnostics;
using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

var three = One + One + One;
var four = One + One + One + One;

var sw = Stopwatch.StartNew();

// Console.WriteLine((three + One).Reciprocal());
Console.WriteLine(Two / three);

Console.WriteLine(sw.ElapsedMilliseconds);