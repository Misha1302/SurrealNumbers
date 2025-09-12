using System.Diagnostics;
using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

SurrealNumberVerifier.Implementation = new SurrealNumberVerifierStub();

var sw = Stopwatch.StartNew();

var q = -SurHalf / Three;
Console.WriteLine(q);

Console.WriteLine(sw.ElapsedMilliseconds);