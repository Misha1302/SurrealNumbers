using System.Diagnostics;
using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

var three = One + One + One;
var four = One + One + One + One;

var reciprocalToOneFour = (three + One).Reciprocal();

var sw = Stopwatch.StartNew();

Console.WriteLine(SurHalf.Reciprocal());
Console.WriteLine((three + One).Reciprocal());
Console.WriteLine(reciprocalToOneFour.Reciprocal());
Console.WriteLine((Two * Two * Two * Two * Three).Reciprocal());
Console.WriteLine(Two / three);

Console.WriteLine(sw.ElapsedMilliseconds);