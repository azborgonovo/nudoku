# Nudoku

A C#/.NET Web API that generates and solves Sudoku puzzles. It provides endpoints to create Sudoku puzzles of varying difficulty levels and solve them programmatically.

## Performance optimizations

There are different Sudoku [solvers](https://github.com/azborgonovo/nudoku/tree/main/src/Nudoku.Engine/Solvers) implemented in the application. They are identified by a version number. Our goal is to optimize them for performance and memory allocation. The current benchmarking results are as follows.

![Benchmark results from 2024-12-05](https://github.com/azborgonovo/nudoku/blob/main/docs/img/Benchmarks_20241205.png?raw=true)

## Running locally

- Install the [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Run the following command from the folder where Nudoku is cloned
```bash
dotnet run --project src/Nudoku.WebApi/Nudoku.WebApi.csproj
```

## Acknowledgements

This experiment was inspired by the [Judoku: The Judo of Sudoku](https://github.com/jetpants/judoku) created and maintained by [Steve Ball](https://github.com/jetpants).
