using System.Collections.Immutable;

namespace Nudoku.Engine;

public record Grid : IGrid
{
    public const int EmptyCellValue = 0;

    public int Size { get; init; }
    public int BoxWidth { get; init; }
    public int BoxHeight { get; init; }
    public ImmutableArray<int> Cells { get; init; }
    
    public Grid(int size, int boxWidth, int boxHeight, int[] cells)
    {
        if (boxWidth * boxHeight != size)
            throw new ArgumentException("Box size must match the size of the grid.");

        if (cells.Length != size * size)
            throw new ArgumentException("Cells array must match the size of the grid.");

        Size = size;
        BoxWidth = boxWidth;
        BoxHeight = boxHeight;
        Cells = [..cells];
    }

    public static Grid Create(int size, int[] cells)
    {
        if (cells.Length != size * size)
            throw new ArgumentException("Cells array must match the size of the grid.");

        var boxSize = (int)Math.Sqrt(size);
        
        return new Grid(size, boxSize, boxSize, cells);
    }
    
    public static Grid Create(int boxWidth, int boxHeight, int[] cells)
    {
        var size = boxWidth * boxHeight;
        
        return new Grid(size, boxWidth, boxHeight, cells);
    }
    
    public static Grid Create(int[,] cells)
    {
        var dimension0Length = cells.GetLength(0);

        if (dimension0Length != cells.GetLength(1))
            throw new ArgumentException("Cells array must be square.", nameof(cells));
        
        var boxSize = (int)Math.Sqrt(dimension0Length);
        
        return Create(boxSize, boxSize, cells);
    }
    
    public static Grid Create(int boxWidth, int boxHeight, int[,] cells)
    {
        var size = boxWidth * boxHeight;
        
        return new Grid(size, boxWidth, boxHeight, cells.Cast<int>().ToArray());
    }

    public int GetCell(int index) => Cells[index];

    public int GetCell(int column, int row) => Cells[GetIndex(column, row)];

    private int GetIndex(int column, int row)
    {
        if (column < 0 || column >= Size)
            throw new ArgumentOutOfRangeException(nameof(column), "Column index out of range.");
        
        if (row < 0 || row >= Size)
            throw new ArgumentOutOfRangeException(nameof(row), "Row index out of range.");
        
        return row * Size + column;
    }

    public bool IsEmpty(int index) => GetCell(index) == EmptyCellValue;

    public bool IsEmpty(int column, int row) => GetCell(column, row) == EmptyCellValue;

    public bool IsFilled(int index) => !IsEmpty(index);

    public bool IsFilled(int column, int row) => !IsEmpty(column, row);

    public IGrid WithCell(int index, int value)
    {
        var newCells = Cells.SetItem(index, value);
        return this with { Cells = newCells };
    }

    public IGrid WithCell(int column, int row, int value) => WithCell(GetIndex(column, row), value);

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
        var hasDuplicatesInAnyRowOrColumn = Enumerable.Range(0, Size)
            .Any(i => HasDuplicates(GetRow(i)) || HasDuplicates(GetColumn(i)));
        
        var hasDuplicatesInAnyBox = Enumerable.Range(0, Size / BoxHeight)
            .SelectMany(boxRow => Enumerable
                .Range(0, Size / BoxWidth)
                .Select(boxCol => GetBox(boxRow * BoxHeight, boxCol * BoxWidth)))
            .Any(HasDuplicates);
        
        var hasZombies = HasZombies();
        
        return !hasDuplicatesInAnyRowOrColumn && !hasDuplicatesInAnyBox && !hasZombies;
    }
    
    private static bool HasDuplicates(int[] values) => values.Where(v => v != EmptyCellValue).GroupBy(v => v).Any(g => g.Count() > 1);

    private bool HasZombies()
    {
        for (int index = 0; index < Cells.Length; index++)
        {
            if (!IsEmpty(index))
                continue;

            var options = GetOptions(index);

            // If there are no possible options, the grid has zombies
            if (options.Count == 0)
                return true;
        }

        // No zombies found
        return false;
    }

    public List<int> GetOptions(int index)
    {
        int size = Size;
        int boxWidth = BoxWidth;
        int boxHeight = BoxHeight;

        var row = index / size;
        var col = index % size;

        var usedValues = new HashSet<int>();

        // Collect values from the row
        for (int c = 0; c < size; c++)
        {
            usedValues.Add(GetCell(c, row));
        }

        // Collect values from the column
        for (int r = 0; r < size; r++)
        {
            usedValues.Add(GetCell(col, r));
        }

        // Collect values from the box
        int boxStartRow = (row / boxHeight) * boxHeight;
        int boxStartCol = (col / boxWidth) * boxWidth;

        for (int r = 0; r < boxHeight; r++)
        {
            for (int c = 0; c < boxWidth; c++)
            {
                int boxCellRow = boxStartRow + r;
                int boxCellCol = boxStartCol + c;
                usedValues.Add(GetCell(boxCellCol, boxCellRow));
            }
        }

        // Return all values not used in the row, column, or box
        return Enumerable.Range(1, size).Where(v => !usedValues.Contains(v)).ToList();
    }

    
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