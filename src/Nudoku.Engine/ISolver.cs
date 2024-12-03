namespace Nudoku.Engine;

public interface ISolver
{
    /// <summary>
    /// Finds a single solution to the puzzle
    /// </summary>
    IGrid? FindSolution(IGrid puzzle);
    
    /// <summary>
    /// Finds all solutions up to the specified maximum
    /// </summary>
    IEnumerable<IGrid> FindSolutions(IGrid puzzle, int max);
    
    /// <summary>
    /// Counts the number of solutions up to the specified maximum
    /// </summary>
    int CountSolutions(IGrid puzzle, int max);
    
    /// <summary>
    /// Checks if the solution is unique
    /// </summary>
    bool IsUnique(IGrid puzzle);
    
    /// <summary>
    /// Checks if the puzzle is minimal
    /// </summary>
    bool IsMinimal(IGrid puzzle);

    /// <summary>
    /// A proper puzzle has exactly one unique solution and no clue is superfluous.
    /// I.e., removing any one of the clues would result in a puzzle with more than
    /// one solution. Proper puzzles are both unique and minimal.
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    bool IsProper(IGrid puzzle) => IsUnique(puzzle) && IsMinimal(puzzle);
}