namespace Solidsoft.Reply.Parsers.Gs1Ai;

using Solidsoft.Reply.Parsers.Gs1Ai.Relationships;

/// <summary>
///   Represents semantics options for GS1 Application Identifiers.
/// </summary>
/// <param name="GtinSemantics">The semantics options for GTIN (Global Trade Item Number).</param>
/// <param name="ExpiryDateSemantics">The semantics options for Expiry Date.</param>
public record struct Semantics(
    GtinSemantics GtinSemantics = GtinSemantics.General,
    ExpiryDateSemantics ExpiryDateSemantics = ExpiryDateSemantics.General) {
}
