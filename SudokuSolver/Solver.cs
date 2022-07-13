using System.Text;
using Common.Exceptions;

namespace SudokuSolver;

public class Solver
{
    private const int Size = 81;

    static Solver()
    {
        CellGroups = new int[Size][];

        var byCell = Enumerable.Range(0, CellGroups.GetLength(0)).ToDictionary(x => x, x => GetGroups(x).ToArray());

        var byGroup = (
            from c in byCell
            from g in c.Value
            group new { Cell = c.Key } by g
            into grp
            select new
            {
                Group = grp.Key,
                Cells = grp.Select(x => x.Cell).ToArray()
            }).ToDictionary(x => x.Group, x => x.Cells);

        for (int i = 0; i < byCell.Count; i++)
        {
            CellGroups[i] = byCell[i].SelectMany(x => byGroup[x]).Where(x => x != i).ToArray();
        }
    }

    private static int[][] CellGroups { get; }

    private static IReadOnlyCollection<char> ValidValues { get; } = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public string Solve(string input)
    {
        string result = input;

        bool done;
        do
        {
            int pre = result.Count(IsValid);

            result = TrySolveByExclusion(result);
            if (result.All(IsValid))
            {
                return result;
            }

            result = TrySolveByBruteForce(result);
            if (result.All(IsValid))
            {
                return result;
            }

            int post = result.Count(IsValid);
            done = post.Equals(pre);
        } while (!done);

        return result;
    }

    private static IEnumerable<string> GetGroups(int pos)
    {
        int row = pos / 9;
        yield return $"r{row}";

        int col = pos % 9;
        yield return $"c{col}";

        int grp = row / 3 * 3 + col / 3;
        yield return $"g{grp}";
    }

    private static bool IsValid(char c) => ValidValues.Contains(c);

    private List<char>[] GetCandidates(IReadOnlyList<char> input)
    {
        var candidates = Enumerable.Range(0, input.Count).Select(x => new List<char>(ValidValues)).ToArray();
        for (int i = 0; i < input.Count; i++)
        {
            if (!IsValid(input[i]))
            {
                continue;
            }

            char value = input[i];
            foreach (int j in CellGroups[i])
            {
                candidates[j].Remove(value);
            }

            candidates[i].Clear();
        }

        return candidates;
    }

    private string TrySolveByBruteForce(string input)
    {
        char[] buffer = input.ToCharArray();
        var candidates = GetCandidates(buffer);
        var stilValid = new List<char>();

        for (int i = 0; i < input.Length; i++)
        {
            stilValid.Clear();

            foreach (char candidate in candidates[i])
            {
                var tmp = new StringBuilder(buffer.Length);
                tmp.Append(buffer, 0, i);
                tmp.Append(candidate);
                tmp.Append(buffer, i + 1, buffer.Length - i - 1);

                try
                {
                    // Förhoppningen är att denna skall ge fel på alla värden förutom ett
                    TrySolveByExclusion(tmp.ToString());
                    stilValid.Add(candidate);
                }
                catch (NotSolvableException) { }

                if (stilValid.Count > 1)
                {
                    break;
                }
            }

            if (stilValid.Count == 1)
            {
                buffer[i] = stilValid.Single();
            }
        }

        return new string(buffer);
    }

    private string TrySolveByExclusion(string input)
    {
        char[] buffer = input.ToCharArray();
        var candidates = GetCandidates(buffer);

        var addedValues = new Queue<int>(16);
        var handledCells = new HashSet<int>(input.Length);

        bool done = false;
        while (!done)
        {
            done = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (candidates[i].Count == 1)
                {
                    char value = candidates[i].Single();

                    if (CellGroups[i].Any(j => buffer[j] == value))
                    {
                        throw new NotSolvableException();
                    }

                    buffer[i] = value;
                    candidates[i].Clear();

                    addedValues.Enqueue(i);
                }
            }

            while (addedValues.TryDequeue(out int pos))
            {
                if (!handledCells.Add(pos))
                {
                    addedValues.Enqueue(pos);
                }

                foreach (int j in CellGroups[pos])
                {
                    candidates[j].Remove(buffer[pos]);

                    if (candidates[j].Count == 0 && !IsValid(buffer[j]))
                    {
                        throw new NotSolvableException();
                    }
                }

                done = false;
            }
        }

        return new string(buffer);
    }
}