namespace Nudoku.Engine;

public interface IGrid
{
    int Size { get; }
    int BoxWidth { get; }
    int BoxHeight { get; }
    int[,] Cells { get; }
    
    
    int GetCell(int index);
    int GetCell(int column, int row);
    bool IsEmpty(int index);
    bool IsEmpty(int column, int row);
    bool IsFilled(int index);
    bool IsFilled(int column, int row);

    IGrid WithCell(int index, int value);
    IGrid WithCell(int column, int row, int value);

    IGrid WithEmpty(int index);
    IGrid WithEmpty(int column, int row);
}