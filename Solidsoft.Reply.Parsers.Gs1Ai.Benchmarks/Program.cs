using BenchmarkDotNet.Running;

namespace Solidsoft.Reply.Parsers.Gs1Ai.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ParserBenchmarks>();
    }
}
