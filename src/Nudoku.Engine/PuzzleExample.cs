namespace Nudoku.Engine;

public class PuzzleExample
{
    public static readonly IGrid Puzzle9X9 = new Grid(new[,]
    {
        { 4, 0, 0, 0, 0, 0, 0, 0, 8 },
        { 8, 7, 0, 0, 0, 0, 0, 5, 6 },
        { 0, 0, 9, 0, 0, 0, 0, 0, 0 },
            
        { 0, 0, 0, 0, 4, 0, 0, 0, 0 },
        { 0, 0, 3, 1, 0, 7, 6, 0, 0 },
        { 0, 4, 0, 0, 6, 0, 0, 7, 0 },
            
        { 0, 1, 5, 0, 0, 0, 9, 4, 0 },
        { 0, 0, 0, 0, 0, 0, 5, 0, 0 },
        { 0, 2, 0, 3, 0, 5, 0, 1, 0 }
    });
    
    public static readonly IGrid Solution9X9 = new Grid(new[,]
    {
        { 4, 3, 1, 5, 2, 6, 7, 9, 8 },
        { 8, 7, 2, 9, 3, 1, 4, 5, 6 },
        { 5, 6, 9, 7, 8, 4, 2, 3, 1 },
        { 7, 5, 6, 8, 4, 3, 1, 2, 9 },
        { 2, 9, 3, 1, 5, 7, 6, 8, 4 },
        { 1, 4, 8, 2, 6, 9, 3, 7, 5 },
        { 3, 1, 5, 6, 7, 8, 9, 4, 2 },
        { 9, 8, 7, 4, 1, 2, 5, 6, 3 },
        { 6, 2, 4, 3, 9, 5, 8, 1, 7 }
    });
        
    public static readonly IGrid Puzzle6X6 = new Grid(new[,]
    {
        { 0, 0, 0, 1, 4, 0 },
        { 0, 0, 5, 0, 0, 0 },
        { 0, 2, 0, 0, 3, 0 },
        { 0, 5, 0, 0, 2, 0 },
        { 0, 0, 0, 4, 0, 0 },
        { 0, 3, 6, 0, 0, 0 }
    });
    
    public static readonly IGrid Solution6X6 = new Grid(new int[,]
    {
        { 3, 6, 2, 1, 4, 5 },
        { 2, 4, 5, 6, 1, 3 },
        { 1, 2, 4, 5, 3, 6 },
        { 6, 5, 1, 3, 2, 4 },
        { 5, 1, 3, 4, 6, 2 },
        { 4, 3, 6, 2, 5, 1 }
    });

    public static readonly IGrid Puzzle4X4 = new Grid(new [,]
    {
        { 1, 0, 0, 0 },
        { 0, 4, 0, 2 },
        { 2, 0, 3, 0 },
        { 0, 0, 0, 1 }
    });
    
    public static readonly IGrid Solution4X4 = new Grid(new [,]
    {
        { 1, 2, 4, 3 },
        { 3, 4, 1, 2 },
        { 2, 1, 3, 4 },
        { 4, 3, 2, 1 }
    });
}