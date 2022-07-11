using System.Text;
using Common;

namespace SudokuSolver;

public class Solver
{
    private readonly ISudokuCell[] _cells = new ISudokuCell[81];

    public Solver SetDefaultValues(string startValues)
    {
        Guard.FromNull(startValues);

        if (startValues.Length != 81)
        {
            throw new ArgumentException(paramName: nameof(startValues), message: $"Invalid length ({startValues.Length})");
        }

        for (int i = 0; i < 81; i++)
        {
            _cells[i] = CreateSudokuCell(startValues[i], i);
        }

        return this;
    }
    
    public string Solve()
    {
        var startValues = (
            from defaultCell in _cells.OfType<DefaultSudokuCell>()
            from cellGroup in defaultCell.Groups
            select new { defaultCell.Value, Group = cellGroup }).ToList();

        foreach (var startValue in startValues)
        {
            var groupMembers = _cells.OfType<SudokuCell>().Where(x => x.Groups.Contains(startValue.Group));
            foreach (SudokuCell groupMember in groupMembers)
            {
                groupMember.PossibleValues.Remove(startValue.Value);
            }
        }

        while (SolveLastPossibleValue() ||
               SolveByTesting()) { }

        return _cells.Aggregate(
            new StringBuilder(_cells.Length),
            (sb, c) => sb.Append(c.Value),
            sb => sb.ToString());
    }

    private static ISudokuCell CreateSudokuCell(char value, int pos)
    {
        ISudokuCell cell = char.IsNumber(value)
            ? new DefaultSudokuCell { Pos = pos, Value = value }
            : new SudokuCell(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' }) { Pos = pos, Value = ' ' };

        int row = pos / 9;
        cell.Groups.Add($"row_{row}");

        int col = pos % 9;
        cell.Groups.Add($"col_{col}");

        int grp = row / 3 * 3 + col / 3;
        cell.Groups.Add($"grp_{grp}");

        return cell;
    }

    private void ExcludeValueFromGroupMembers(ISudokuCell cell)
    {
        foreach (string cellGroup in cell.Groups)
        {
            var groupCells =
                from c in _cells.OfType<SudokuCell>()
                where c.PossibleValues.Any()
                where c.Groups.Contains(cellGroup)
                select c;

            foreach (SudokuCell otherCell in groupCells)
            {
                otherCell.PossibleValues.Remove(cell.Value);
            }
        }
    }

    private bool SolveByTesting()
    {
        return false;
    }

    private bool SolveLastPossibleValue()
    {
        var canBeSolved = (
            from cell in _cells.OfType<SudokuCell>()
            where cell.HasValue == false
            where cell.PossibleValues.Count == 1
            select cell).ToList();

        if (canBeSolved.Count == 0)
        {
            return false;
        }

        foreach (SudokuCell cell in canBeSolved)
        {
            cell.Value = cell.PossibleValues.Single();
            cell.PossibleValues.Clear();

            ExcludeValueFromGroupMembers(cell);
        }

        return true;
    }
}