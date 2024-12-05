using System.Text;

namespace Nudoku.Engine;

public record Grid : IGrid
{
    public const int EmptyCellValue = 0;

    public int Size { get; }
    public int BoxWidth { get; }
    public int BoxHeight { get; }
    public int[] Cells { get; }

    public Grid(int size, int[] cells)
    {
        if (cells.Length != size * size)
            throw new ArgumentException("Cells array must match the size of the grid.");

        Size = size;
        BoxWidth = (int)Math.Sqrt(size);
        BoxHeight = (int)Math.Sqrt(size);
        Cells = (int[])cells.Clone();
    }

    public Grid(int[,] cells)
    {
        if (cells.GetLength(0) != cells.GetLength(1))
            throw new ArgumentException("Cells array must be square.");

        Size = cells.GetLength(0);
        BoxWidth = (int)Math.Sqrt(Size);
        BoxHeight = (int)Math.Sqrt(Size);
        Cells = cells.Cast<int>().ToArray();
    }

    public int GetCell(int index) => Cells[index];

    public int GetCell(int column, int row) => Cells[row * Size + column];

    public bool IsEmpty(int index) => GetCell(index) == EmptyCellValue;

    public bool IsEmpty(int column, int row) => GetCell(column, row) == EmptyCellValue;

    public bool IsFilled(int index) => !IsEmpty(index);

    public bool IsFilled(int column, int row) => !IsEmpty(column, row);

    public IGrid WithCell(int index, int value)
    {
        var newCells = (int[])Cells.Clone();
        newCells[index] = value;
        return new Grid(Size, newCells);
    }

    public IGrid WithCell(int column, int row, int value) => WithCell(row * Size + column, value);

    public IGrid WithEmpty(int index) => WithCell(index, EmptyCellValue);

    public IGrid WithEmpty(int column, int row) => WithCell(column, row, EmptyCellValue);
    
    public int[,] GetMatrix()
    {
        var matrix = new int[Size, Size];
        for (var i = 0; i < Size; i++)
        {
            for (var j = 0; j < Size; j++)
            {
                matrix[i, j] = GetCell(j, i);
            }
        }
        return matrix;
    }

    public bool IsViable()
    {
        return !Enumerable.Range(0, Size).Any(i => HasDuplicates(GetRow(i)) || HasDuplicates(GetColumn(i))) &&
               !Enumerable.Range(0, Size / BoxHeight)
                          .SelectMany(boxRow => Enumerable
                              .Range(0, Size / BoxWidth)
                              .Select(boxCol => GetBox(boxRow * BoxHeight, boxCol * BoxWidth)))
                          .Any(HasDuplicates);
    }
    
    private static bool HasDuplicates(int[] values) => values.Where(v => v != EmptyCellValue).GroupBy(v => v).Any(g => g.Count() > 1);

    private int[] GetRow(int row) => Cells.Skip(row * Size).Take(Size).ToArray();

    private int[] GetColumn(int column) => Enumerable.Range(0, Size).Select(row => Cells[row * Size + column]).ToArray();

    private int[] GetBox(int startRow, int startColumn)
    {
        return Enumerable.Range(0, BoxHeight)
                         .SelectMany(r => Enumerable
                             .Range(0, BoxWidth)
                             .Select(c => Cells[(startRow + r) * Size + startColumn + c]))
                         .ToArray();
    }
}