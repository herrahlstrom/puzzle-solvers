namespace SudokuSolver;

internal interface ISudokuCell
{
    public ICollection<string> Groups { get; }
    public bool HasValue { get; }
    public int Pos { get; }
    char Value { get; }
}

internal class DefaultSudokuCell : ISudokuCell
{
    public ICollection<string> Groups { get; } = new List<string>(3);
    public bool HasValue => true;
    public int Pos { get; init; }
    public char Value { get; init; }
}

internal class SudokuCell : ISudokuCell
{
    public SudokuCell(IEnumerable<char> possibleValues)
    {
        PossibleValues = new List<char>(possibleValues);
    }

    public ICollection<string> Groups { get; } = new List<string>(3);
    public bool HasValue => Value != ' ';

    public int Pos { get; init; }

    public ICollection<char> PossibleValues { get; }
    public char Value { get; set; }
}