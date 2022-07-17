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


void PrintSudoku(string input, string result)
{
    const string template = @"+-----+-----+-----+
+ ### + ### + ### +
+ ### + ### + ### +
+ ### + ### + ### +
+-----+-----+-----+
+ ### + ### + ### +
+ ### + ### + ### +
+ ### + ### + ### +
+-----+-----+-----+
+ ### + ### + ### +
+ ### + ### + ### +
+ ### + ### + ### +
+-----+-----+-----+";

    ConsoleColor bkFg = Console.ForegroundColor;
    for (int i = 0, j = 0; j < template.Length; j++)
    {
        if (template[j] == '#')
        {
            if (result[i] == input[i])
            {
                Console.Write(result[i]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(result[i]);
                Console.ForegroundColor = bkFg;
            }

            i++;
        }
        else
        {
            Console.Write(template[j]);
        }
    }

    Console.WriteLine();
}