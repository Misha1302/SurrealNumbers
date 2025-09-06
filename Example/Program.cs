using SurrealNumber;

var minOne = SurrealNumberCache.MinusOne;
var zero = SurrealNumberCache.Zero;
var one = SurrealNumberCache.One;

var arr = (Span<SurrealNum>) [minOne, zero, one];

foreach (var a in arr)
foreach (var b in arr)
    Console.WriteLine($"{a.To<double>()} {b.To<double>()} {a.Add(b).To<double>()} {a.Add(b).To<string>()}");

var acc = zero;
for (var i = 0; i <= 5; i++)
    Console.WriteLine($"{i}: {(acc += one).To<string>()}");