﻿using SudokuSolver;

// Beginner
var beginnerResult = new Solver()
    .SetDefaultValues("-3-824-692---37-8-78-5-932-----6--9767329--4--28--5---4-718-9-23-547--16-1295--7-")
    .Solve();
PrintSolution("Beginner", beginnerResult);

// Easy
var easyResult = new Solver()
    .SetDefaultValues("78--9--4-4-61---98-93---2-7---5196---5-----299-462-3512---35---56948-7---179--4-5")
    .Solve();
PrintSolution("Easy", easyResult);

//Hard
var hardResult = new Solver()
    .SetDefaultValues("8---9--5--5---4------815-24-7---1--81--37---2-4---8731--------6-3-54-----16-27-8-")
    .Solve();
PrintSolution("Hard", hardResult);

// Expert
var expertREsult = new Solver()
    .SetDefaultValues("--2-1-63--3---9--419----2---65----8-------5-2---38---1---2--------9-71-5-2--3-4--")
    .Solve();
PrintSolution("Expert", expertREsult);

void PrintSolution(string name, string result)
{
    Console.WriteLine(name);
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