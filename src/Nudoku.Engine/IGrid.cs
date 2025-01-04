using System.Collections.Immutable;

namespace Nudoku.Engine;

public interface IGrid
{
    int Size { get; }
    int BoxWidth { get; }
    int BoxHeight { get; }
    ImmutableArray<int> Cells { get; }
    
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

    List<int> GetOptions(int index);
    
    int[,] GetMatrix();

    /// <summary>
    /// If the grid has any duplicate values in any group or if there are cells that have
    /// no possible allowed values then an incorrect move has been previously made and the
    /// grid has no solution.
    /// </summary>
    bool IsViable();
}