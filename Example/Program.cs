using System.Diagnostics;
using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

var reciprocalToOneFour = (Three + One).Reciprocal();

var sw = Stopwatch.StartNew();

Console.WriteLine(SurHalf.Reciprocal());
Console.WriteLine((Three + One).Reciprocal().Reciprocal());
Console.WriteLine(reciprocalToOneFour.Reciprocal());
Console.WriteLine((Two * Two * Two * Two * Three).Reciprocal());
Console.WriteLine(Two / Three);

Console.WriteLine(sw.ElapsedMilliseconds);