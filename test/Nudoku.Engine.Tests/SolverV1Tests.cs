using Nudoku.Engine.Solvers;

namespace Nudoku.Engine.Tests;

public class SolverV1Tests
{
    [Theory]
    [MemberData(nameof(TestCases_For_FindSolution_Should_ReturnExpectedSolution))]
    public void FindSolution_Should_ReturnExpectedSolution(IGrid puzzle, IGrid expectedSolution)
    {
        // Act
        var solver = new SolverV1();
        var solution = solver.FindSolution(puzzle);

        // Assert
        Assert.NotNull(solution);
        Assert.Equal(expectedSolution.Cells, solution.Cells);
    }

    public static IEnumerable<object[]> TestCases_For_FindSolution_Should_ReturnExpectedSolution()
    {        
        return new List<object[]>
        {
            new object[] { PuzzleExample.Puzzle4X4, PuzzleExample.Solution4X4 },
            new object[] { PuzzleExample.Puzzle6X6, PuzzleExample.Solution6X6 },
            new object[] { PuzzleExample.Puzzle9X9, PuzzleExample.Solution9X9 }
        };
    }
}