using Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

namespace Solidsoft.Reply.Parsers.Gs1Ai;
#if NET5_0_OR_GREATER
public record struct Semantics(
    GtinSemantics GtinSemantics = GtinSemantics.General,
    ExpiryDateSemantics ExpiryDateSemantics = ExpiryDateSemantics.General) {
    }
#else
public struct Semantics() {
    public GtinSemantics GtinSemantics { get; } = GtinSemantics.General;
    public ExpiryDateSemantics ExpiryDateSemantics { get; } = ExpiryDateSemantics.General;
}
#endif
