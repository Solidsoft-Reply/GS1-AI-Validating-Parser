using Solidsoft.Reply.Parsers.Common;

using Xunit;

namespace Solidsoft.Reply.Parsers.Gs1Ai.Tests;

#if NET7_0_OR_GREATER

public class AdditionalTests {
    private readonly ResolvedElementDelegate _resolvedEntityDelegate;
    private readonly ResolvedElementDelegate _resolvedEntityDelegateNoErrorsRef;
    private readonly ResolvedElementDelegate _resolvedEntityDelegateWith2101For01ErrorRef;

    public AdditionalTests() {
        _resolvedEntityDelegate = new(ResolvedEntityDelegate);
        _resolvedEntityDelegateNoErrorsRef = new(ResolvedEntityDelegateNoErrorsRef);
        _resolvedEntityDelegateWith2101For01ErrorRef = new(ResolvedEntityDelegateWith2101For01ErrorRef);
    }

    [Fact]
    public void Parse_Gs1String() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3'];
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegate);
    }

    [Fact]
    public void Parse_Gs1StringWithInvalidFnc1Termination() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', Convert.ToChar(29), '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3'];
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegate);
    }

    [Fact]
    public void Parse_Gs1StringWithInvalidFinalFnc1Termination() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3', Convert.ToChar(29)];
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegate);
    }

    [Fact]
    public void Parse_Gs1StringWithValidGCPCheck() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3', Convert.ToChar(29), '2', '1', '4', '1', '0', '9', '0', '6', '7', '2', '1', '8', '8', '7', '0', '8', '3', '6'];
        List<string> gcps = new() { "509257", "1234567", "95200000" };

        Parser.Parse(sampleGs1Data, ResolvedEntityDelegateNoErrors, gcps: gcps);
    }

    [Fact]
    public void Parse_Gs1StringWithInvalidGCPCheck() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3', Convert.ToChar(29), '2', '1', '4', '1', '0', '9', '0', '6', '7', '2', '1', '8', '8', '7', '0', '8', '3', '6'];
        List<string> gcps = new() { "509257", "76543210", "95200000" };
        Parser.Parse(sampleGs1Data, ResolvedEntityDelegateWith2101For01Error, gcps: gcps);
    }

    [Fact]
    public void ParseEx_Gs1StringWithValidGCPCheck() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3', Convert.ToChar(29), '2', '1', '4', '1', '0', '9', '0', '6', '7', '2', '1', '8', '8', '7', '0', '8', '3', '6'];
        List<string> gcps = new() { "509257", "1234567", "95200000" };
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegateNoErrorsRef, gcps: gcps);
    }

    [Fact]
    public void ParseEx_Gs1StringWithInvalidGCPCheck() {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3', Convert.ToChar(29), '2', '1', '4', '1', '0', '9', '0', '6', '7', '2', '1', '8', '8', '7', '0', '8', '3', '6'];
        List<string> gcps = new() { "509257", "76543210", "95200000" };
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegateWith2101For01ErrorRef, gcps: gcps);
    }


    private void ResolvedEntityDelegate(in ResolvedApplicationIdentifierRef entity) {
        //_results.Add(entity);
    }

    private void ResolvedEntityDelegateNoErrors(IResolvedEntity entity) {
        Assert.False(entity.IsError);
        Assert.False(entity.IsFatal);
        Assert.False(entity.Exceptions.Any());
    }

    private void ResolvedEntityDelegateWith2101For01Error(IResolvedEntity entity) {
        if (entity.Entity == 1) {
            Assert.True(entity.IsError);
            Assert.True(entity.IsFatal);
            Assert.Contains(entity.Exceptions, e => e.ErrorNumber == 2101);
            return;
        }

        return;
    }

    private void ResolvedEntityDelegateNoErrorsRef(in ResolvedApplicationIdentifierRef entity) {
        Assert.False(entity.IsError);
        Assert.False(entity.IsFatal);
        Assert.False(entity.Exceptions.Any());
    }

    private void ResolvedEntityDelegateWith2101For01ErrorRef(in ResolvedApplicationIdentifierRef entity) {
        if (entity.Entity == 1) {
            Assert.True(entity.IsError);
            Assert.True(entity.IsFatal);
            Assert.Contains(entity.Exceptions, e => e.ErrorNumber == 2101);
                return;
        }

        return;
    }
}
#endif