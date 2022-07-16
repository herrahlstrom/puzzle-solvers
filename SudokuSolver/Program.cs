using System.Diagnostics;
using SudokuSolver;

var solver = new Solver();

while (Console.ReadLine() is { } input)
{
    var sw = Stopwatch.StartNew();
    string result = solver.Solve(input);
    sw.Stop();

    Console.WriteLine("Solve time: {0} ms.", sw.ElapsedMilliseconds);

    PrintSudoku(input, result);

    Console.WriteLine();
}

void PrintSudoku(params string[] value)
{
    for (int i = 0; i < value.Length; i++)
    {
        if (i > 0)
        {
            Console.Write("\t");
        }

        Console.Write("+-----+-----+-----+");
    }

    Console.WriteLine();

    for (int start = 0; start < 27; start += 9)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (i > 0)
            {
                Console.Write("\t");
            }

            string s = value[i];

            Console.Write("+ ");
            Console.Write(s.Substring(start, 3));
            Console.Write(" + ");
            Console.Write(s.Substring(start + 3, 3));
            Console.Write(" + ");
            Console.Write(s.Substring(start + 6, 3));
            Console.Write(" +");
        }

        Console.WriteLine();
    }

    for (int i = 0; i < value.Length; i++)
    {
        if (i > 0)
        {
            Console.Write("\t");
        }

        Console.Write("+-----+-----+-----+");
    }

    Console.WriteLine();

    for (int start = 27; start < 54; start += 9)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (i > 0)
            {
                Console.Write("\t");
            }

            string s = value[i];
            Console.Write("+ ");
            Console.Write(s.Substring(start, 3));
            Console.Write(" + ");
            Console.Write(s.Substring(start + 3, 3));
            Console.Write(" + ");
            Console.Write(s.Substring(start + 6, 3));
            Console.Write(" +");
        }

        Console.WriteLine();
    }

    for (int i = 0; i < value.Length; i++)
    {
        if (i > 0)
        {
            Console.Write("\t");
        }

        Console.Write("+-----+-----+-----+");
    }

    Console.WriteLine();

    for (int start = 54; start < 81; start += 9)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (i > 0)
            {
                Console.Write("\t");
            }

            string s = value[i];
            Console.Write("+ ");
            Console.Write(s.Substring(start, 3));
            Console.Write(" + ");
            Console.Write(s.Substring(start + 3, 3));
            Console.Write(" + ");
            Console.Write(s.Substring(start + 6, 3));
            Console.Write(" +");
        }

        Console.WriteLine();
    }

    for (int i = 0; i < value.Length; i++)
    {
        if (i > 0)
        {
            Console.Write("\t");
        }

        Console.Write("+-----+-----+-----+");
    }

    Console.WriteLine();
}