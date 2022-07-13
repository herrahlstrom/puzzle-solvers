using System.Diagnostics;
using SudokuSolver;

string[] inputs =
{
    "-3-824-692---37-8-78-5-932-----6--9767329--4--28--5---4-718-9-23-547--16-1295--7-",
    "78--9--4-4-61---98-93---2-7---5196---5-----299-462-3512---35---56948-7---179--4-5",
    "8---9--5--5---4------815-24-7---1--81--37---2-4---8731--------6-3-54-----16-27-8-",
    "--2-1-63--3---9--419----2---65----8-------5-2---38---1---2--------9-71-5-2--3-4--"
};

var initSw = Stopwatch.StartNew();
Solver solver = new Solver();
initSw.Stop();

Console.WriteLine("Init {0}, {1} ms.", solver.GetType().Name, initSw.ElapsedMilliseconds);

for (int i = 0; i < inputs.Length; i++)
{
    var sw = Stopwatch.StartNew();
    string result = solver.Solve(inputs[i]);
    sw.Stop();

    Console.WriteLine();
    Console.WriteLine("#{0,2}, {1} ms.", i + 1, sw.ElapsedMilliseconds);
    Console.WriteLine("+-----+-----+-----+");

    for (int start = 0; start < 27; start += 9)
    {
        Console.Write("+ ");
        Console.Write(result.Substring(start, 3));
        Console.Write(" + ");
        Console.Write(result.Substring(start + 3, 3));
        Console.Write(" + ");
        Console.Write(result.Substring(start + 6, 3));
        Console.Write(" +");
        Console.WriteLine();
    }

    Console.WriteLine("+-----+-----+-----+");
    for (int start = 27; start < 54; start += 9)
    {
        Console.Write("+ ");
        Console.Write(result.Substring(start, 3));
        Console.Write(" + ");
        Console.Write(result.Substring(start + 3, 3));
        Console.Write(" + ");
        Console.Write(result.Substring(start + 6, 3));
        Console.Write(" +");
        Console.WriteLine();
    }

    Console.WriteLine("+-----+-----+-----+");
    for (int start = 54; start < 81; start += 9)
    {
        Console.Write("+ ");
        Console.Write(result.Substring(start, 3));
        Console.Write(" + ");
        Console.Write(result.Substring(start + 3, 3));
        Console.Write(" + ");
        Console.Write(result.Substring(start + 6, 3));
        Console.Write(" +");
        Console.WriteLine();
    }

    Console.WriteLine("+-----+-----+-----+");

    Console.WriteLine();
}