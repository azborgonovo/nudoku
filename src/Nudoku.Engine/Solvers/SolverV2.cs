namespace Nudoku.Engine.Solvers;

public class SolverV2 : ISolver
{
    public IGrid? FindSolution(IGrid puzzle)
    {
        var solutions = new List<IGrid>();
        SolveAll(puzzle, 1, solutions);
        return solutions.FirstOrDefault();
    }

    public IEnumerable<IGrid> FindSolutions(IGrid puzzle, int max)
    {   
        var solutions = new List<IGrid>();
        SolveAll(puzzle, max, solutions);
        return solutions;
    }

    public int CountSolutions(IGrid puzzle, int max)
    {
        var solutions = new List<IGrid>();
        SolveAll(puzzle, max, solutions);
        return solutions.Count;
    }

    public bool IsUnique(IGrid puzzle)
    {
        return CountSolutions(puzzle, 2) == 1;
    }

    public bool IsMinimal(IGrid puzzle)
    {
        for (var i = 0; i < puzzle.Cells.Length; i++)
        {
            if (!puzzle.IsFilled(i))
                continue;

            var reducedPuzzle = puzzle.WithEmpty(i);
            if (IsUnique(reducedPuzzle))
                return false;
        }
        return true;
    }

    private static void SolveAll(IGrid puzzle, int max, List<IGrid> solutions)
    {
        if (solutions.Count >= max)
            return;

        var emptyCell = FindEmptyCell(puzzle);
        if (emptyCell is null)
        {
            solutions.Add(puzzle);
            return;
        }

        foreach (var value in GetPossibleValues(puzzle, emptyCell.Value))
        {
            var newPuzzle = puzzle.WithCell(emptyCell.Value, value);
            SolveAll(newPuzzle, max, solutions);
        }
    }

    private static int? FindEmptyCell(IGrid puzzle)
    {
        for (int i = 0; i < puzzle.Cells.Length; i++)
        {
            if (puzzle.IsEmpty(i))
                return i;
        }
        return null;
    }

    private static IEnumerable<int> GetPossibleValues(IGrid puzzle, int cell)
    {
        int size = puzzle.Size;
        int boxWidth = puzzle.BoxWidth;
        int boxHeight = puzzle.BoxHeight;

        var row = cell / size;
        var col = cell % size;

        var usedValues = new HashSet<int>();

        // Add values from the row
        for (int c = 0; c < size; c++)
        {
            usedValues.Add(puzzle.GetCell(c, row));
        }

        // Add values from the column
        for (int r = 0; r < size; r++)
        {
            usedValues.Add(puzzle.GetCell(col, r));
        }

        // Add values from the box
        int boxStartRow = (row / boxHeight) * boxHeight;
        int boxStartCol = (col / boxWidth) * boxWidth;

        for (int r = 0; r < boxHeight; r++)
        {
            for (int c = 0; c < boxWidth; c++)
            {
                usedValues.Add(puzzle.GetCell(boxStartCol + c, boxStartRow + r));
            }
        }

        return Enumerable.Range(1, size).Where(v => !usedValues.Contains(v));
    }
}
