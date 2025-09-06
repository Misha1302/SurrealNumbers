using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

var arr = (SurrealNum[]) [MinusOne, Zero, MinusOne + One, One, One + One];
var res = arr.SelectMany(_ => arr, (a, b) => a + b);
Console.WriteLine(string.Join(", ", res.Select(x => x.To<double>())));