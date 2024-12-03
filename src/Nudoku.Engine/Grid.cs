namespace Nudoku.Engine;

using System;
public record Grid : IGrid
{
    public const int DefaultSize = 9;
    public const int EmptyCellValue = 0;
    
    public int Size { get; init; }
    public int[,] Cells { get; init; }
    public int BoxWidth  { get; init; }
    public int BoxHeight  { get; init; }

    public Grid(int size = DefaultSize)
    {
        if (size <= 0 || Math.Sqrt(size) % 1 != 0)
            throw new ArgumentException("Size must be a perfect square.");
        
        BoxWidth = (int)Math.Sqrt(size);
        BoxHeight = BoxWidth;
        
        Size = size;
        Cells = new int[Size, Size];
    }
    
    public Grid(int[,] cells)
    {
        int size = cells.GetLength(0);

        if (cells.GetLength(0) != cells.GetLength(1) || Math.Sqrt(size) % 1 != 0)
            throw new ArgumentException("Cells must represent a square grid with a perfect square size.");
        
        Size = size;
        Cells = (int[,])cells.Clone(); // Ensure immutability
    }

    public Grid(int boxWidth, int boxHeight)
    {
        if (boxWidth <= 0 || boxHeight <= 0)
            throw new ArgumentException("Box width and height must be positive.");
        
        Size = boxWidth * boxHeight;
        Cells = new int[Size, Size];
        BoxWidth = boxWidth;
        BoxHeight = boxHeight;
    }
    
    public int NumColumns => Size;
    public int NumRows => Size;
    public int NumBoxes => Size;
    public int NumCells => Size * Size;
    public int NumStacks => BoxHeight; // Width of the grid in boxes
    public int NumBands => BoxWidth; // Height of the grid in boxes
    public int NumEmptyCells => NumCells - NumFilledCells;
    public int NumFilledCells => Cells.Cast<int>().Count(cell => cell != EmptyCellValue);
    public int ToIndex(int column, int row) => (row - 1) * NumColumns + column;
    public int ToColumn(int index) => (index - 1) % NumColumns + 1;
    public int ToRow(int index) => (index - 1) / NumColumns + 1;

    public int GetCell(int index) => GetCell(ToColumn(index), ToRow(index));
    public int GetCell(int column, int row) => Cells[row, column];
    public bool IsEmpty(int index) => GetCell(index) == EmptyCellValue;
    public bool IsEmpty(int column, int row) => GetCell(column, row) == EmptyCellValue;
    public bool IsFilled(int index) => !IsEmpty(index);
    public bool IsFilled(int column, int row) => !IsEmpty(column, row);
    public IGrid WithCell(int index, int value) => WithCell(ToColumn(index), ToRow(index), value);
    public IGrid WithCell(int column, int row, int value)
    {
        if (value < 0 || value > Size)
            throw new ArgumentException("Value must be between 0 and the grid size.");

        var newCells = (int[,])Cells.Clone();
        newCells[row, column] = value;
        return this with { Cells = newCells };
    }
    public IGrid WithEmpty(int index) => WithCell(index, 0);
    public IGrid WithEmpty(int column, int row) => WithCell(column, row, 0);
}
