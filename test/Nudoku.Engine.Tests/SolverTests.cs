namespace Nudoku.Engine.Tests;

public class SolverTests
{
    [Fact]
    public void FindSolution_Should_ReturnCorrectValidSolution()
    {
        // Arrange
        var puzzle = new int[,]
        {
            { 4, 3, 0, 0 },
            { 1, 2, 3, 0 },
            { 0, 0, 2, 0 },
            { 2, 1, 0, 0 }
        };
        var grid = new Grid(puzzle);
        
        
        
        // Act
        var solver = new Solver();
        var solution = solver.FindSolution(grid);

        // Assert
        var expectedSolution = new int[,]
        {
            { 4, 3, 1, 2 },
            { 1, 2, 3, 4 },
            { 3, 4, 2, 1 },
            { 2, 1, 4, 3 }
        };
        Assert.NotNull(solution);
        Assert.Equal(expectedSolution, solution.Cells);
    }
}