# Benchmarks

This console app uses [BenchmarkDotNet](https://benchmarkdotnet.org/) to track the performance of the Nudoku.Engine project.

## How to run the benchmarks

1. Open a terminal window and navigate to the root of the Nudoku.Engine.Benchmarks project.
2. Run the following command to execute all benchmarks:

```bash
dotnet run -c Release -- -f '*'
```

The results will be displayed in the terminal window.