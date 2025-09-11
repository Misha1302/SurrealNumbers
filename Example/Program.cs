using System.Diagnostics;
using SurrealNumber;
using static SurrealNumber.SurrealCacheNumbers;

// Console.WriteLine(string.Join(", ", SurrealNumsCreator.GenerateNumbersForBirthday(3)));
// Console.WriteLine(Zero + SurHalf);

// return;

// Console.WriteLine(One + Two);
// Console.WriteLine(string.Join(", ", q));

// var reciprocalToOneFour = (Three + One).Reciprocal();
//
// Console.WriteLine(Two + Two);

// Console.WriteLine((Five + Five + Five) * (SurHalf * SurHalf));
// Console.WriteLine((Five + Five + Five - One) * (SurHalf * SurHalf));
//
// var a = (Five + Five + One) * SurHalf;
// var b = (Five + Five + Two) * One;
// var c = Five + Five + One;
// var d = a + b;
// Console.WriteLine(d);
// Console.WriteLine(d - c);
// return;
// Console.WriteLine(Five + Five + Two);
// Console.WriteLine((Five + Five + Two) * (SurHalf * SurHalf));
// Console.WriteLine((Five + Five + One) * (SurHalf * SurHalf));
// Console.WriteLine((Five + Five) * (SurHalf * SurHalf));
// Console.WriteLine((Three + Three) * (SurHalf * SurHalf));
// Console.WriteLine((Five) * (SurHalf * SurHalf));
// Console.WriteLine((Four) * (SurHalf * SurHalf));
// Console.WriteLine((Three) * (SurHalf * SurHalf));
// Console.WriteLine((Two) * (SurHalf * SurHalf));
// Console.WriteLine((One) * (SurHalf * SurHalf));
// Console.WriteLine(Two * SurHalf);
// Console.WriteLine(SurHalf + One);
// return;


Console.WriteLine(Two + Two);
Console.WriteLine(Three + Four);
Console.WriteLine(Three - Four);
Console.WriteLine(Five * Three);

Console.WriteLine(SurHalf.Reciprocal());
var surrealNum = (Three + One).Reciprocal();
Console.WriteLine(surrealNum);
Console.WriteLine((Three + One).Reciprocal().Reciprocal());
Console.WriteLine(Two / Three);

var sw = Stopwatch.StartNew();

Console.WriteLine((Two * Two * Two * Two * Three).Reciprocal());

Console.WriteLine(sw.ElapsedMilliseconds);