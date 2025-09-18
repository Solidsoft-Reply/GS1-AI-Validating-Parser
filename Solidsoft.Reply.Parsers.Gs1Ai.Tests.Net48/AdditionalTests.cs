namespace Solidsoft.Reply.Parsers.Gs1Ai.Tests;

#if NET7_0_OR_GREATER

public class AdditionalTests {
    private readonly ResolvedEntityDelegate _resolvedEntityDelegate;

    public AdditionalTests() {
        _resolvedEntityDelegate = new(ResolvedEntityDelegate);
    }

    [Fact]
    public void Parse_Gs1String() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3'];
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegate);
            
    }
    private void ResolvedEntityDelegate(in ResolvedApplicationIdentifierRef entity) {
        //_results.Add(entity);
    }
}
#endif
