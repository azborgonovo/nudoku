using BenchmarkDotNet.Running;

// Adding support for BenchmarkDotNet args
// See: https://benchmarkdotnet.org/articles/guides/console-args.html
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);