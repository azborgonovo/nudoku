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
        var boxSize = (int)Math.Sqrt(size);
        return Generate(size, boxSize, boxSize);
    }

    public IGrid Generate(int boxWidth, int boxHeight)
    {
        var size = boxWidth * boxHeight;
        return Generate(size, boxWidth, boxHeight);
    }

    public IGrid Generate(int size, int boxWidth, int boxHeight)
    {
        var solvedGrid = GenerateSolvedGrid(size, boxWidth, boxHeight);
        return CreatePuzzle(solvedGrid);
    }

    private IGrid GenerateSolvedGrid(int size, int boxWidth, int boxHeight)
    {
        var emptyGrid = new Grid(boxWidth, boxHeight, new int[size * size]);
        var solvedGrid = _solver.FindSolution(emptyGrid);

        if (solvedGrid == null)
            throw new InvalidOperationException("Failed to generate a solvable grid.");

        return solvedGrid;
    }

    private static IGrid CreatePuzzle(IGrid solvedGrid)
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

        return new Grid(solvedGrid.BoxWidth, solvedGrid.BoxHeight, cells);
    }
}