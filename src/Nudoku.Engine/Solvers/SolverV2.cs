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

        var possibleValues = puzzle.GetOptions(emptyCell.Value);
        foreach (var value in possibleValues)
        {
            var newPuzzle = puzzle.WithCell(emptyCell.Value, value);
            SolveAll(newPuzzle, max, solutions);
        }
    }

    private static int? FindEmptyCell(IGrid puzzle)
    {
        for (var i = 0; i < puzzle.Cells.Length; i++)
        {
            if (puzzle.IsEmpty(i))
                return i;
        }
        return null;
    }
}
