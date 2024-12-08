namespace Nudoku.Engine.Solvers;

public class SolverV1 : ISolver
{
    public IGrid? FindSolution(IGrid puzzle)
    {
        var solutions = new List<IGrid>();
        SolveAll(puzzle, solutions, 1);
        return solutions.FirstOrDefault();
    }
    
    public IEnumerable<IGrid> FindSolutions(IGrid puzzle, int max)
    {
        var solutions = new List<IGrid>();
        SolveAll(puzzle, solutions, max);
        return solutions;
    }
    
    public int CountSolutions(IGrid puzzle, int max)
    {
        var solutions = new List<IGrid>();
        SolveAll(puzzle, solutions, max, stopAfterCount: true);
        return solutions.Count;
    }
    
    public bool IsUnique(IGrid puzzle)
    {
        var solutions = new List<IGrid>();
        SolveAll(puzzle, solutions, 2);
        return solutions.Count == 1;
    }
    
    public bool IsMinimal(IGrid puzzle)
    {
        for (int row = 0; row < puzzle.Size; row++)
        {
            for (int col = 0; col < puzzle.Size; col++)
            {
                if (!puzzle.IsEmpty(col, row))
                {
                    var reducedGrid = puzzle.WithEmpty(col, row);
                    if (IsUnique(reducedGrid))
                        return false; // Removing a clue still results in a unique solution
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Finds all solutions recursively and adds them to the solutions list
    /// </summary>
    private static void SolveAll(IGrid grid, List<IGrid> solutions, int max, bool stopAfterCount = false)
    {
        if (solutions.Count >= max)
            return;

        for (var row = 0; row < grid.Size; row++)
        {
            for (var col = 0; col < grid.Size; col++)
            {
                if (!grid.IsEmpty(col, row))
                    continue;
                
                for (var num = 1; num <= grid.Size; num++)
                {
                    if (!IsSafe(grid, col, row, num))
                        continue;
                    
                    var newGrid = grid.WithCell(col, row, num);

                    SolveAll(newGrid, solutions, max, stopAfterCount);
                    
                    if (stopAfterCount && solutions.Count >= max)
                        return;
                }
                return; // Backtrack
            }
        }
        solutions.Add(grid);
    }

    /// <summary>
    /// Checks if a number can be safely placed at a specific cell
    /// </summary>
    private static bool IsSafe(IGrid grid, int col, int row, int num)
    {
        return !UsedInRow(grid, row, num) &&
               !UsedInColumn(grid, col, num) &&
               !UsedInBox(grid, col, row, num);
    }

    private static bool UsedInRow(IGrid grid, int row, int num)
    {
        for (int col = 0; col < grid.Size; col++)
            if (grid.GetCell(col, row) == num)
                return true;

        return false;
    }

    private static bool UsedInColumn(IGrid grid, int col, int num)
    {
        for (int row = 0; row < grid.Size; row++)
            if (grid.GetCell(col, row) == num)
                return true;

        return false;
    }

    private static bool UsedInBox(IGrid grid, int col, int row, int num)
    {
        int startRow = row / grid.BoxHeight * grid.BoxHeight;
        int startCol = col / grid.BoxWidth * grid.BoxWidth;

        for (int r = 0; r < grid.BoxHeight; r++)
        {
            for (int c = 0; c < grid.BoxWidth; c++)
            {
                if (grid.GetCell(startCol + c, startRow + r) == num)
                    return true;
            }
        }

        return false;
    }
}