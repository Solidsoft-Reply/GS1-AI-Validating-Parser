using BenchmarkDotNet.Attributes;

using Solidsoft.Reply.Parsers.Common;

namespace Solidsoft.Reply.Parsers.Gs1Ai.Benchmarks;

[MemoryDiagnoser]
[WarmupCount(3)]
[IterationCount(10)]
public class ParserBenchmarks
{
    private static readonly string[] Fnc1ElementStrings =
    [
        "01095011015300061731123110ABC123\u001d21SN000111222333",
        "010001234567890510LOT42\u001d1731123121SERIAL-XYZ12",
    ];

    private static readonly Action<IResolvedEntity> ProcessResolvedEntityAction = entity =>
    {
        _ = entity.Identifier;
        _ = entity.Value;
    };

#if NET7_0_OR_GREATER
    private static readonly ResolvedElementDelegate ProcessResolvedEntityDelegate = (scoped in ResolvedApplicationIdentifierRef entity) =>
    {
        _ = entity.Identifier;
        _ = entity.Value;
    };
#endif

    [ParamsSource(nameof(GetElementStrings))]
    public string Data { get; set; } = string.Empty;

    public IEnumerable<string> GetElementStrings() => Fnc1ElementStrings;

    [Params(DataRelationshipTests.None, DataRelationshipTests.InvalidPairs, DataRelationshipTests.All)]
    public DataRelationshipTests RelationshipTests { get; set; } = DataRelationshipTests.None;

    [Benchmark(Description = "Parse(string) with callback")]
    public void Parse_String()
    {
        Parser.Parse(
            Data,
            ProcessResolvedEntityAction,
            initialPosition: 0,
            relationshipTests: RelationshipTests,
            semantics: default);
    }

#if NET6_0_OR_GREATER
    [Benchmark(Description = "Parse(ReadOnlySpan<char>) with callback")]
    public void Parse_Span()
    {
        Parser.Parse(
            Data.AsSpan(),
            ProcessResolvedEntityAction,
            initialPosition: 0,
            relationshipTests: RelationshipTests,
            semantics: default);
    }
#endif

#if NET7_0_OR_GREATER
    [Benchmark(Description = "ParseEx(ReadOnlySpan<char>) with delegate")]
    public void ParseEx_Span()
    {
        Parser.ParseEx(
            Data.AsSpan(),
            ProcessResolvedEntityDelegate,
            initialPosition: 0,
            relationshipTests: RelationshipTests,
            semantics: default);
    }
#endif

    private static readonly IList<string> MultipleBarcodes =
    [
        "(01)09501101530006(17)250101(10)ABC123",
        "(21)SN0001",
        "(01)09501101530006"
    ];

    [Benchmark(Description = "Parse(List<string>) full relationship tests")]
    public void ParseMulti_FullRelationships()
    {
        Parser.Parse(
            MultipleBarcodes,
            ProcessResolvedEntityAction,
            semantics: default);
    }
}