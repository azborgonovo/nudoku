using BenchmarkDotNet.Attributes;

namespace Nudoku.Engine.Benchmarks;

[MemoryDiagnoser]
public class SolverBenchmarks
{
    private readonly Solver _solver;
    
    private readonly Grid _puzzle2X2;
    private readonly Grid _puzzle3X3;

    public SolverBenchmarks()
    {
        var puzzle2X2 = new int[,]
        {
            { 4, 3, 0, 0 },
            { 1, 2, 3, 0 },
            { 0, 0, 2, 0 },
            { 2, 1, 0, 0 }
        };
        _puzzle2X2 = new Grid(puzzle2X2);
        
        var puzzle3X3 = new int[, ]
        {
            { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };
        _puzzle3X3 = new Grid(puzzle3X3);
        
        _solver = new Solver();
    }
    
    [Benchmark]
    public void Solve2X2PuzzleV1() => _solver.FindSolution(_puzzle2X2);
    
    [Benchmark]
    public void Solve3X3PuzzleV1() => _solver.FindSolution(_puzzle3X3);
}