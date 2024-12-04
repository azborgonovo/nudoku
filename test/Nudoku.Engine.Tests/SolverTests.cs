namespace Nudoku.Engine.Tests;

public class SolverTests
{
    [Theory]
    [MemberData(nameof(TestCases_For_FindSolution_Should_ReturnExpectedSolution))]
    public void FindSolution_Should_ReturnExpectedSolution(int[,] puzzle, int expected)
    {
        // Arrange
        var grid = new Grid(puzzle);

        // Act
        var solver = new Solver();
        var solution = solver.FindSolution(grid);

        // Assert
        Assert.NotNull(solution);
    }

    public static IEnumerable<object[]> TestCases_For_FindSolution_Should_ReturnExpectedSolution()
    {
        var puzzle9X9 = new int[,]
        {
            { 4, 0, 0, 0, 0, 0, 0, 0, 8 },
            { 8, 7, 0, 0, 0, 0, 5, 6, 0 },
            { 0, 0, 9, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 4, 0, 0, 0, 0, 0 },
            { 3, 1, 7, 0, 0, 6, 0, 0, 0 },
            { 4, 0, 6, 0, 0, 0, 7, 0, 0 },
            { 1, 5, 0, 0, 0, 9, 0, 4, 0 },
            { 2, 3, 0, 0, 0, 0, 5, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 7, 1 }
        };
        
        var puzzle6X6 = new int[,]
        {
            { 0, 0, 1, 4, 0, 0 },
            { 5, 0, 0, 0, 0, 3 },
            { 2, 0, 3, 0, 0, 0 },
            { 0, 5, 0, 0, 0, 0 },
            { 0, 0, 4, 0, 2, 0 },
            { 3, 6, 0, 0, 0, 0 }
        };
        
        var puzzle4X4 = new int[,]
        {
            { 1, 4, 0, 2 },
            { 2, 3, 0, 0 },
            { 0, 0, 3, 1 },
            { 0, 0, 2, 1 }
        };
        
        return new List<object[]>
        {
            new object[] { puzzle4X4, 0 },
            new object[] { puzzle6X6, 0 },
            new object[] { puzzle9X9, 0 }
        };
    }
}