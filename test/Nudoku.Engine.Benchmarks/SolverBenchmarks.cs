using BenchmarkDotNet.Attributes;
using Nudoku.Engine.Solvers;

namespace Nudoku.Engine.Benchmarks;

[MemoryDiagnoser]
public class SolverBenchmarks
{
    private readonly SolverV1 _solverV1;
    private readonly SolverV2 _solverV2;

    public SolverBenchmarks()
    {
        _solverV1 = new SolverV1();
        _solverV2 = new SolverV2();
    }
    
    [Benchmark]
    public void Solve4X4PuzzleV1() => _solverV1.FindSolution(PuzzleExample.Puzzle4X4);
    
    [Benchmark]
    public void Solve6X6PuzzleV1() => _solverV1.FindSolution(PuzzleExample.Puzzle6X6);

    [Benchmark]
    public void Solve4X4PuzzleV2() => _solverV2.FindSolution(PuzzleExample.Puzzle4X4);
    
    [Benchmark]
    public void Solve6X6PuzzleV2() => _solverV2.FindSolution(PuzzleExample.Puzzle6X6);
}