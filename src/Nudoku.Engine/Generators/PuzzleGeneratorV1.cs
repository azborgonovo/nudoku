namespace Nudoku.Engine.Generators;

public class PuzzleGeneratorV1 : IPuzzleGenerator
{
    private readonly ISolver _solver;
    private static readonly Random Random = new();

    public PuzzleGeneratorV1(ISolver solver)
    {
        _solver = solver;
    }

    public IGrid Generate(int size)
    {
        var boxWidth = (int)Math.Sqrt(size);
        var boxHeight = boxWidth;
        return Generate(boxWidth, boxHeight);
    }

    public IGrid Generate(int boxWidth, int boxHeight)
    {
        var size = boxWidth * boxHeight;
        
        var solvedGrid = GenerateSolvedGrid(size);
        
        return CreatePuzzle(solvedGrid, size);
    }

    private IGrid GenerateSolvedGrid(int size)
    {
        var emptyGrid = new Grid(size, new int[size * size]);
        var solvedGrid = _solver.FindSolution(emptyGrid);

        if (solvedGrid == null)
            throw new InvalidOperationException("Failed to generate a solvable grid.");

        return solvedGrid;
    }

    private static IGrid CreatePuzzle(IGrid solvedGrid, int size)
    {
        var cells = solvedGrid.Cells.ToArray();
        var totalCells = cells.Length;
        var cellsToRemove = totalCells / 2;

        while (cellsToRemove > 0)
        {
            var index = Random.Next(totalCells);
            if (cells[index] != Grid.EmptyCellValue)
            {
                cells[index] = Grid.EmptyCellValue;
                cellsToRemove--;
            }
        }

        return new Grid(size, cells);
    }
}